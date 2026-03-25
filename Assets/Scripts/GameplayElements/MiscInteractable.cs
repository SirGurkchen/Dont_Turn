public class MiscInteractable : InteractableController
{
    public override void Interact(PlayerController player)
    {
        player.SetThought(_interactData.InteractText);
    }
}
