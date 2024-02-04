using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    public int slotId;
    public Image image;

    public void UpdateSlot(bool isInUsed)
    {
        if (isInUsed)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.grey;
        }
    }
}
