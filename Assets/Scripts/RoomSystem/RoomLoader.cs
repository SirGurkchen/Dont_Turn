using System;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private RoomDataBase _roomDataBase;

    private GameObject _currentRoom;
    private DoorLogic _currentDoor;

    public event Action OnDoorWasUsed;

    public GameObject CurrentRoom => _currentRoom;

    public void LoadNewRoom(int roomNumber)
    {
        if (_currentDoor != null)
        {
            _currentDoor.OnDoorInteract -= DoorInteract;
        }

        _currentRoom = _roomDataBase.ActivateNewRoom(roomNumber);
        if (_currentRoom != null)
        {
            if (_currentRoom.GetComponentInChildren<DoorLogic>())
            {
                 _currentDoor = _currentRoom.GetComponentInChildren<DoorLogic>();
                _currentDoor.OnDoorInteract += DoorInteract;
            }
            else
            {
                Debug.Log("No Door found in room number: " + roomNumber);
            }
        }
    }

    private void DoorInteract()
    {
        OnDoorWasUsed?.Invoke();
    }
}
