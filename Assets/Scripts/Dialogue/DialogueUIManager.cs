using TMPro;
using UnityEngine;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speakingTextArea;
    [SerializeField] private TextMeshProUGUI _speakerNameArea;

    public void ShowSpeakingUI(string speaker, string text)
    {
        _speakingTextArea.text = text;
        _speakerNameArea.text = speaker;
    }
}
