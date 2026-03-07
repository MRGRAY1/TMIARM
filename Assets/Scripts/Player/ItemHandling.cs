using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandling : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;
    private GameItems currentItem => Managers.Instance.GameManager.CurrentItem;

    private void OnEnable()
    {
        GameEvents.UpdateItemInHand += UpdateItem;
    }

    private void UpdateItem(object obj)
    {
        switch (currentItem)
        {
            case GameItems.EmptyHanded:
                ChangeItemInHand(0);
                break;
            case GameItems.Wrench:
                ChangeItemInHand(1);
                break;
            case GameItems.Hammer:
                ChangeItemInHand(2);
                break;
            case GameItems.ScrewDriver:
                ChangeItemInHand(3);
                break;
            case GameItems.Knife:
                ChangeItemInHand(4);
                break;
            case GameItems.Multimeter:
                ChangeItemInHand(5);
                break;
        }
    }

    private void ChangeItemInHand(int newItem)
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }

        items[newItem].SetActive(true);
    }
}