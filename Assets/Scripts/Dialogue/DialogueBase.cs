using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Entry", menuName = "Dialogue/Entry")]
public class DialogueBase : ScriptableObject
{
    [SerializeField] private string _speakerName;
    [TextArea]
    [SerializeField] private string _spokenText;
}
