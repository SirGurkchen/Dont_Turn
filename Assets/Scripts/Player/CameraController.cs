using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensitivity = 100f;

    private float _xRotation = 0f;
    private Quaternion _defaultLook;
    private Quaternion _defaultBodyRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _defaultLook = Quaternion.identity;
        _defaultBodyRotation = _playerBody.rotation;
    }

    private void Update()
    {
        Vector2 lookInput = GameInput.Instance.PlayerLook();

        float mouseX = lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ResetCam()
    {
        _xRotation = 0f;
        transform.localRotation = _defaultLook;
        _playerBody.rotation = _defaultBodyRotation;
    }
}
