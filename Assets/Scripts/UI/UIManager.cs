using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private TextMeshProUGUI _roomText;
    [SerializeField] private TextMeshProUGUI _interactText;

    private string _defaultRoomText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _defaultRoomText = "Room ";
    }

    public void ShowNextRoom(int roomNumber)
    {
        _roomText.text = _defaultRoomText + roomNumber;
        _blackScreen.SetActive(true);
    }

    public void HideBlackScreen()
    {
        _blackScreen.SetActive(false);
    }

    public void UpdatePrompt(string text)
    {
        _interactText.text = text;
    }
}
