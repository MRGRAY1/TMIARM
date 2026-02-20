using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Summary Of File
/// </summary>

// ToDo:
// - Add specific logic or initialization here
public class InteractableBehaviors : MonoBehaviour, IInteractable
{
    #region Variables
    // Declare fields, constants, and serialized variables here.
    [Header("References")]
    [SerializeField] private List<GameObject> requiredObjects;
    [SerializeField] private bool isInteractable;

    public bool IsInteractable
    {
        get => isInteractable;
        set
        {
            if (isInteractable == value)
                return;

            isInteractable = value;
        }
    }


    #endregion

    #region Functions
    // Called before Start().
    // Initialize references and set up components.
    private void Awake()
    {

        UpdateInteractionState();

    }


    public void Interact()
    {
        if (!IsInteractable)
        {
            return;
        }
        Logger.Log($"{this} interacted with");
    }

    public void UpdateInteractionState()
    {
        if (requiredObjects == null || requiredObjects.Count == 0)
        {
            IsInteractable = true;
        }
        foreach (var obj in requiredObjects)
        {
            if (obj.TryGetComponent<IInteractable>(out var interactable))
            {
                if (!interactable.IsInteractable)
                {
                    IsInteractable = false;
                    return;
                }
            }
        }
        IsInteractable = true;
    }

    #endregion
}
