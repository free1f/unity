using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    public int slotId;
    public Image image;

    public void UpdateSlot(InventoryState state)
    {
        switch (state)
        {
            case InventoryState.Empty:
                image.color = Color.grey;
                break;
            case InventoryState.Full:
                image.color = Color.cyan;
                break;
            case InventoryState.InUse:
                image.color = Color.green;
                break;
        }
    }
}
