using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerRotateCheck : MonoBehaviour
{
    [SerializeField] private Volume _vignetteVolume;
    private Vector3 _startForward;
    private bool _isLookingBehind;
    private bool _shouldTrigger;
    private Vignette _vignette;

    public event Action OnLookBehind;

    private const float LOOK_BEHIND_TRIGGER_VALUE = -0.35f;

    private void Start()
    {
        _isLookingBehind = false;
        _shouldTrigger = false;
        _startForward = transform.forward;

        if (_vignetteVolume.profile.TryGet<Vignette>(out var vignette))
        {
            _vignette = vignette;
        }
    }

    private void Update()
    {
        float dot = Vector3.Dot(_startForward, transform.forward);

        if (_vignette != null && _shouldTrigger)
        {
            _vignette.smoothness.value = Mathf.Clamp01(-dot * 2.25f);
            _vignette.smoothness.overrideState = true;
        }

        if (dot < LOOK_BEHIND_TRIGGER_VALUE && _shouldTrigger)
        {
            if (!_isLookingBehind)
            {
                OnStartedLookingBehind();
            }
            _isLookingBehind = true;
        }
        else
        {
            _isLookingBehind = false;
        }
    }

    private void OnStartedLookingBehind()
    {
        OnLookBehind?.Invoke();
    }

    public void ActivateLookBehind()
    {
        _shouldTrigger = true;
    }
}
