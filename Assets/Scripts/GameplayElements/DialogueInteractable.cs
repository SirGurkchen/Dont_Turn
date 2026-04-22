using UnityEngine;
using System;

public class DialogueInteractable : InteractableController
{
    [SerializeField] private DialogueBase _dialogueData;
    [SerializeField] private AudioClip _talkAudio;
    [SerializeField] private NPCAnimator _animator;

    public event Action<DialogueContext> OnDialogueInteract;

    public override void Interact(PlayerController player)
    {
        DialogueContext context = new DialogueContext(_dialogueData.SpeakerName, _dialogueData.SpokenText, _talkAudio, _animator);
        OnDialogueInteract?.Invoke(context);
    }

    private void OnDestroy()
    {
        OnDialogueInteract = null;
    }
}

public readonly struct DialogueContext
{
    public string Speaker { get; }
    public string[] Text { get; }
    public AudioClip SpeakAudio { get; }
    public NPCAnimator NPCAnimation { get; }

    public DialogueContext(string speaker, string[] text, AudioClip sound, NPCAnimator animator)
    {
        Speaker = speaker;
        Text = text;
        SpeakAudio = sound;
        NPCAnimation = animator;
    }
}
