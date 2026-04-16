using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions _inputActions;

    public event Action OnInteract;
    public event Action OnDialogueProgress;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("There are multiple Game Inputs!");
            Destroy(gameObject);
            return;
        }

        _inputActions = new InputActions();
        Instance = this;
    }

    private void Start()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Dialogue.Disable();
        _inputActions.Player.Interact.performed += InteractPerformed;
        _inputActions.Player.Dialogue.performed += DialoguePerformed;
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void DialoguePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDialogueProgress?.Invoke();
    }

    public Vector2 PlayerMovementNormalized()
    {
        Vector2 inputVec = _inputActions.Player.Move.ReadValue<Vector2>();
        return inputVec.normalized;
    }

    public void TogglePlayerMovement()
    {
        if (_inputActions.Player.Move.enabled)
        {
            _inputActions.Player.Move.Disable();
            _inputActions.Player.Look.Disable();
        }
        else
        {
            _inputActions.Player.Move.Enable();
            _inputActions.Player.Look.Enable();
        }
    }

    public void TogglePlayerDialogue()
    {
        if (_inputActions.Player.Move.enabled)
        {
            _inputActions.Player.Move.Disable();
            _inputActions.Player.Look.Disable();
            _inputActions.Player.Interact.Disable();
            _inputActions.Player.Dialogue.Enable();
        }
        else
        {
            _inputActions.Player.Move.Enable();
            _inputActions.Player.Look.Enable();
            _inputActions.Player.Enable();
            _inputActions.Player.Dialogue.Disable();
        }
    }

    public Vector2 PlayerLook()
    {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Interact.performed -= InteractPerformed;
    }

    private void OnDisable()
    {
        OnInteract = null;
    }
}
