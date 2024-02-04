using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public InventorySlotView[] inventorySlotViews;

    private void Start()
    {
        for (int i = 0; i < inventorySlotViews.Length; i++)
        {
            inventorySlotViews[i].slotId = i;
            inventorySlotViews[i].UpdateSlot(false);
        }
    }

    public void UpdateSlotById(int slotId, bool isInUsed)
    {
        var foundSlot = Array.Find(inventorySlotViews, slot => slot.slotId == slotId);
        if (foundSlot != null)
        {
            foundSlot.UpdateSlot(isInUsed);
        }
    }
}
