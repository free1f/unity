#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freelf.Inventory;
using Freelf.Item;
using System;

namespace Freelf.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private BaseItem[] inventory;
        public int itemDefaultLayer;
        
        public InventoryView inventoryView;
        public event Action<BaseItem?> OnEquip;

        private void Start()
        {
            inventory = new BaseItem[inventoryView.inventorySlotViews.Length];
        }

        public void AddItem(BaseItem item, Transform parent)
        {
            // Check if inventory slots are full or not
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null) continue;
                inventory[i].gameObject.SetActive(false);
                inventoryView.UpdateSlotById(i, InventoryState.Full);
            }

            // Add the item
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    inventoryView.UpdateSlotById(i, InventoryState.InUse);
                    item.transform.SetParent(parent);
                    item.gameObject.SetActive(true);
                    item.gameObject.layer = itemDefaultLayer;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    // item.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                }
            }

            OnEquip?.Invoke(item);
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                ChangeItem(0);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)) {
                ChangeItem(1);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)) {
                ChangeItem(2);
            }
            if(Input.GetKeyDown(KeyCode.Alpha4)) {
                ChangeItem(3);
            }
            if(Input.GetKeyDown(KeyCode.Alpha5)) {
                ChangeItem(4);
            }
            if(Input.GetKeyDown(KeyCode.Alpha6)) {
                ChangeItem(5);
            }
        }

        private void ChangeItem(int index) {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (index == i) continue;
                if (inventory[i] == null) continue;
                inventory[i].gameObject.SetActive(false);
                inventoryView.UpdateSlotById(i, InventoryState.Full);
            }
            if(inventory[index] != null) {
                inventory[index].gameObject.SetActive(true);
                inventoryView.UpdateSlotById(index, InventoryState.InUse);
            }
            OnEquip?.Invoke(inventory[index]);
        }

        public BaseItem? CurrentItemInUse()
        {
            var slotIndex =  Array.FindIndex(inventoryView.inventorySlotViews, slot => slot.state == InventoryState.InUse);
            if(slotIndex == -1) return null;
            return inventory[slotIndex];
        }
    }
}