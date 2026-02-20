using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; set; }
    void Interact();
    void UpdateInteractionState();
}
