using System;
using UnityEngine;

public class SpecialDocumentInteractable : DocumentInteractable
{
    private bool _wasUsed = false;

    public static event Action OnSpecialDocumentRead;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        if (!_wasUsed)
        {
            OnSpecialDocumentRead?.Invoke();
            _wasUsed = true;
        }
    }
}
