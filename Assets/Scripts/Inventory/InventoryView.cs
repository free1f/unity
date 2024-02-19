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
            inventorySlotViews[i].UpdateSlot(InventoryState.Empty);
        }
    }

    public void UpdateSlotById(int slotId, InventoryState state)
    {
        var foundSlot = Array.Find(inventorySlotViews, slot => slot.slotId == slotId);
        if (foundSlot != null)
        {
            foundSlot.UpdateSlot(state);
        }
    }
}
