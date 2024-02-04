using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    private BaseItem[] inventory;
    public InventoryView inventoryView;

    private void Start()
    {
        inventory = new BaseItem[inventoryView.inventorySlotViews.Length];
    }

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                inventoryView.UpdateSlotById(i, true);
                return;
            }
        }
    }
}
