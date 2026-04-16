using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Camera _playerCam;
    [SerializeField] private PlayerInteractManager _interaction;
    [SerializeField] private PlayerLook _look;

    private void Start()
    {
        GameInput.Instance.OnInteract += HandleInteract;
        _look.Initialize(_playerCam);
    }

    private void Update()
    {
        _look.Look();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInteract()
    {
        _interaction.DoInteract(this, _look.CurrentInteractable);
    }

    public void SetThought(string thought)
    {
        _interaction.StartThinking(thought);
    }

    public void ShowDialogue(string dialogue)
    {
        _interaction.SetDialogueThought(dialogue);
    }

    private void MovePlayer()
    {
        Vector2 moveDir = GameInput.Instance.PlayerMovementNormalized();
        Vector3 moveVec = transform.TransformDirection(new Vector3(moveDir.x, 0f, moveDir.y));

        _playerRb.linearVelocity = new Vector3(
            moveVec.x * _moveSpeed,
            _playerRb.linearVelocity.y,
            moveVec.z * _moveSpeed
        );
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteract -= HandleInteract;
    }
}
