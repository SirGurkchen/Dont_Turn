using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUIManager _dialogueUI;
    [SerializeField] private DialogueAudioManager _audioManager;

    private List<DialogueInteractable> _interactables;
    private string[] _currentDialogue;
    private int _dialogueCount;
    private int _maxDialogue;
    private string _speaker;
    private NPCAnimator _currentAnimator;
    private AudioClip _currentSpeakerAudio;


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

    private void HandleDialogue(DialogueContext context)
    {
        GameInput.Instance.TogglePlayerDialogue();
        _maxDialogue = context.Text.Length;
        _dialogueCount = 0;
        _currentDialogue = context.Text;
        _speaker = context.Speaker;
        _currentSpeakerAudio = context.SpeakAudio;
        _currentAnimator = context.NPCAnimation;


        if (_dialogueUI != null)
        {
            _audioManager.PlayDialogueAudio(_currentSpeakerAudio);
            _currentAnimator.PlaySpeakAnimation();
            _dialogueUI.ShowSpeakingUI(_speaker, _currentDialogue[_dialogueCount]);
        }
    }

    private void ContinueDialogue()
    {
        if (_dialogueCount == _maxDialogue - 1)
        {
            GameInput.Instance.TogglePlayerDialogue();
            _currentDialogue = null;
            _speaker = string.Empty;
            _currentSpeakerAudio = null;
            _currentAnimator = null;
            _dialogueUI.ShowSpeakingUI(string.Empty, string.Empty);
        }
        else
        {
            _dialogueCount++;
            _audioManager.PlayDialogueAudio(_currentSpeakerAudio);
            _currentAnimator.PlaySpeakAnimation();
            _dialogueUI.ShowSpeakingUI(_speaker, _currentDialogue[_dialogueCount]);
        }
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnDialogueProgress -= ContinueDialogue;
    }
}
