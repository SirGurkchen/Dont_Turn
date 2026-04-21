using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerThoughtText;

    private Coroutine _thoughtRoutine;

    private const float THOUGHT_SHOW_TIME = 1.5f;

    private void Start()
    {
        ClearThought();
    }

    public void DoInteract(PlayerController player, IInteract interactable)
    {
        if (interactable != null)
        {
            interactable.HideInteractPrompt();
            interactable.Interact(player);
        }
    }

    private void SetThoughtText(string text)
    {
        _playerThoughtText.text = text;
    }

    public void StartThinking(string text)
    {
        if (text == _playerThoughtText.text) return;

        if (_thoughtRoutine != null)
        {
            StopCoroutine(_thoughtRoutine);
        }
        _thoughtRoutine = StartCoroutine(Think(text));
    }

    private IEnumerator Think(string text)
    {
        SetThoughtText(text);
        yield return new WaitForSeconds(THOUGHT_SHOW_TIME);
        ClearThought();
    }

    private void ClearThought()
    {
        SetThoughtText(string.Empty);
    }
}
