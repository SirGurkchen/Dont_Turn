using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _speaker;


    public void PlayDialogueAudio(AudioClip sound)
    {
        _speaker.Stop();
        _speaker.PlayOneShot(sound);
    }
}
