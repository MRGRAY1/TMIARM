using System;
using UnityEngine;

namespace Behaviors
{
    public class InventoryBehavior : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.OpenInventory += OpenInventory;
            GameEvents.CloseInventory += CloseInventory;
        }


        private void OnDisable()
        {
            GameEvents.OpenInventory -= OpenInventory;
            GameEvents.CloseInventory -= CloseInventory;
        }

        private void OpenInventory(object obj)
        {
            if (Managers.Instance.GameManager.CurrentState == GameState.Playing)
            {
                Logger.Log("Opening Inventory");
            }
        }

        private void CloseInventory(object obj)
        {
            if (Managers.Instance.GameManager.CurrentState == GameState.Playing)
            {
                Logger.Log("Closing Inventory");
            }
        }
    }
}