using System.Collections;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _characterNeck;
    [SerializeField] private Vector3 _neckRotationOffset;
    [SerializeField] private Animator _animator;

    private GameObject _playerObject;
    private Quaternion _defaultLook;
    private float _blinkTimer;

    private const float LOOK_THRESHOLD = 2.75f;
    private const float LOOK_SPEED = 6.5f;
    private const float MAX_LOOK_ANGLE = 85f;
    
    private float _nextBlinkInterval;

    private void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("PlayerLook");
        _defaultLook = _characterNeck.transform.rotation;
        _nextBlinkInterval = GetNextBlinkInterval();

        if (_playerObject == null)
        {
            Debug.Log("No Player Object assigned!");
        }
    }

    private void Update()
    {
        _blinkTimer += Time.deltaTime;
        if (_blinkTimer >= _nextBlinkInterval)
        {
            _animator.SetTrigger("DoBlink");
            _blinkTimer = 0f;
            _nextBlinkInterval = GetNextBlinkInterval();
        }

        if (CalculatePlayerDistance() > LOOK_THRESHOLD)
        {
            ResetLook();
            return;
        }

        LookAtPlayer();
    }

    public void PlaySpeakAnimation()
    {
        _animator.SetTrigger("DoTalk");
    }

    private float CalculatePlayerDistance()
    {
        return Vector3.Distance(_playerObject.transform.position, gameObject.transform.position);
    }

    private void ResetLook()
    {
        _characterNeck.transform.rotation = Quaternion.Slerp(
            _characterNeck.transform.rotation,
            _defaultLook,
            Time.deltaTime * LOOK_SPEED
        );    
    }

    private void LookAtPlayer()
    {
        if (!IsAbleToLook())
        {
            ResetLook();
            return;
        }

        Vector3 direction = _playerObject.transform.position - _characterNeck.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(_neckRotationOffset);        
        _characterNeck.transform.rotation = Quaternion.Slerp(
            _characterNeck.transform.rotation,
            targetRotation,
            Time.deltaTime * LOOK_SPEED
        );
    }

    private bool IsAbleToLook()
    {
        Vector3 direction = _playerObject.transform.position - _characterNeck.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(_neckRotationOffset);
        return Quaternion.Angle(_defaultLook, targetRotation) < MAX_LOOK_ANGLE;
    }

    private float GetNextBlinkInterval()
    {
        return Random.Range(2f, 5f);
    }
}
