using System;
using UnityEngine;

public class DoorLogic : InteractableController
{
    [SerializeField] private AudioClip _doorSound;

    public event Action OnDoorInteract;

    public override void Interact(PlayerController player)
    {
        AudioManager.Instance.PlayAudioClip(_doorSound);
        OnDoorInteract?.Invoke();
    }

    private void OnDisable()
    {
        OnDoorInteract = null;
    }
}
