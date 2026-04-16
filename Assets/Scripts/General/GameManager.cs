using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private CameraController _playerCam;
    [SerializeField] private UIManager _UI;
    [SerializeField] private PlayerRotateCheck _playerRotate;
    [SerializeField] private RoomLoader _roomLoader;
    [SerializeField] private DialogueManager _dialogueManager;

    private int _roomCount = 1;
    private const float TRANSITION_TIMER = 3f;

    private void Start()
    {
        _roomLoader.OnDoorWasUsed += HandleDoorUse;
        _playerRotate.OnLookBehind += HandleLookBehind;
        SpecialDocumentInteractable.OnSpecialDocumentRead += HandleSpecialDocument;

        _roomLoader.LoadNewRoom(_roomCount);
        _dialogueManager.CheckForDialogueObjects(_roomLoader.CurrentRoom);
    }

    private void HandleSpecialDocument()
    {
        _playerRotate.ActivateLookBehind();
        SpecialDocumentInteractable.OnSpecialDocumentRead -= HandleSpecialDocument;
    }

    private void HandleDoorUse()
    {
        _roomCount++;
        _dialogueManager.ClearDialogueObjects();
        StartCoroutine(DoRoomTransition());
    }

    private void HandleLookBehind()
    {
        Debug.Log("Dead!");
    }

    private IEnumerator DoRoomTransition()
    {
        GameInput.Instance.TogglePlayerMovement();
        _UI.ShowNextRoom(_roomCount);
        _roomLoader.LoadNewRoom(_roomCount);
        _playerObject.transform.position = _startPoint.transform.position;
        _playerCam.ResetCam();
        _dialogueManager.CheckForDialogueObjects(_roomLoader.CurrentRoom);
        yield return new WaitForSeconds(TRANSITION_TIMER);
        GameInput.Instance.TogglePlayerMovement();
        _UI.HideBlackScreen();
    }

    private void OnDestroy()
    {
        _roomLoader.OnDoorWasUsed -= HandleDoorUse;
        _playerRotate.OnLookBehind -= HandleLookBehind;
    }
}
