using UnityEngine;
using System;

public class DialogueInteractable : InteractableController
{
    [SerializeField] private DialogueBase _dialogueData;

    public event Action<string, string[], PlayerController> OnDialogueInteract;

    public override void Interact(PlayerController player)
    {
        OnDialogueInteract?.Invoke(_dialogueData.SpeakerName, _dialogueData.SpokenText, player);
    }

    private void OnDestroy()
    {
        OnDialogueInteract = null;
    }
}
