using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private List<DialogueInteractable> _interactables;

    private void Start()
    {
        _interactables = new List<DialogueInteractable>();
    }

    public void CheckForDialogueObjects(GameObject currentRoom)
    {
        if (currentRoom != null)
        {
            _interactables = currentRoom.GetComponentsInChildren<DialogueInteractable>().AddRange();

            if (_interactables.Count > 0) 
            {
                foreach (DialogueInteractable interactable in _interactables)
                {
                    interactable.OnDialogueInteract += HandleDialogue;
                }
            }
            else
            {
                Debug.Log("No Dialogue Interactables found in this room!");
            }
        }
    }

    public void ClearDialogueObjects()
    {
        foreach (DialogueInteractable interactable in _interactables)
        {
            interactable.OnDialogueInteract -= HandleDialogue;
        }
        _interactables.Clear();
    }

    private void HandleDialogue()
    {
        
    }
}
