using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 1.5f;
    [SerializeField] private LayerMask _interactMask;

    private IInteract _lookInteractable;
    private IInteract _previousInteractable;
    private Camera _camera;

    public IInteract CurrentInteractable => _lookInteractable;

    public void Initialize(Camera _playerCam)
    {
        _camera = _playerCam;
    }

    public void Look()
    {
        if (_camera == null) return;

        _previousInteractable = _lookInteractable;
        _lookInteractable = null;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
        {
            if (hit.collider.TryGetComponent<IInteract>(out IInteract interactable))
            {
                _lookInteractable = interactable;
            }
            else
            {
                _lookInteractable = hit.collider.GetComponentInParent<IInteract>();
            }
        }

        if (_previousInteractable != _lookInteractable)
        {
            if (_previousInteractable != null) _previousInteractable.HideInteractPrompt();
            if (_lookInteractable != null) _lookInteractable.ShowInteractPrompt();
        }
    }
}
