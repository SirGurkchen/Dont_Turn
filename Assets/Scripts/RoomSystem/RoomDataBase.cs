using UnityEngine;

public class RoomDataBase : MonoBehaviour
{
    [SerializeField] private GameObject[] _rooms;

    private GameObject _currentRoom;

    public GameObject ActivateNewRoom(int roomCount)
    {
        if (_rooms.Length < roomCount) return null;
        if (_currentRoom != null) Destroy(_currentRoom);
        _currentRoom = Instantiate(_rooms[roomCount - 1]);
        return _currentRoom;
    }
}
