using UnityEngine;
using UnityEngine.UI;

public class DocumentInteractable : InteractableController
{
    [SerializeField] private Image _shownDocument;

    private bool _isShown = false;

    private void Start()
    {
        _shownDocument.enabled = _isShown;
    }

    public override void Interact(PlayerController player)
    {
        _isShown = !_isShown;
        GameInput.Instance.TogglePlayerMovement();
        _shownDocument.enabled = _isShown;
    }
}
