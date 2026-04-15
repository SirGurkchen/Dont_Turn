using UnityEngine;
using System;

public class DialogueInteractable : InteractableController
{
    public static event Action OnDialogueInteract;

    public override void Interact(PlayerController player)
    {
        OnDialogueInteract?.Invoke();
    }

    private void OnDestroy()
    {
        OnDialogueInteract = null;
    }
}
