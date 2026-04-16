using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private List<DialogueInteractable> _interactables;
    private string[] _currentDialogue;
    private int _dialogueCount;
    private int _maxDialogue;
    private PlayerController _player;


    private void Start()
    {
        _interactables = new List<DialogueInteractable>();
        GameInput.Instance.OnDialogueProgress += ContinueDialogue;
    }

    public void CheckForDialogueObjects(GameObject currentRoom)
    {
        if (currentRoom != null)
        {
            _interactables = currentRoom.GetComponentsInChildren<DialogueInteractable>().ToList<DialogueInteractable>();

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

    private void HandleDialogue(string speaker, string[] text, PlayerController player)
    {
        if (_player == null)
        {
            _player = player;
        }

        GameInput.Instance.TogglePlayerDialogue();
        _maxDialogue = text.Length;
        _dialogueCount = 0;
        _currentDialogue = text;

        if (_player != null)
        {
            _player.ShowDialogue(_currentDialogue[_dialogueCount]);
        }
    }

    private void ContinueDialogue()
    {
        if (_dialogueCount == _maxDialogue - 1)
        {
            GameInput.Instance.TogglePlayerDialogue();
            _player.ShowDialogue(null);
            _currentDialogue = null;
            return;
        }
        else
        {
            _dialogueCount++;
            _player.ShowDialogue(_currentDialogue[_dialogueCount]);
        }
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnDialogueProgress -= ContinueDialogue;
    }
}
