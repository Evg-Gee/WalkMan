using UnityEngine;

public interface IPickUpHandler
{
    IInteractable CurrentItem { get; }
    void SetInteractableItem(IInteractable item);
    void RemoveInteractableItem(IInteractable item);

}