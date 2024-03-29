using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterItemAction : MonoBehaviour
{
    private BaseItem _attachedItem;
    public event Action<BaseItem, Action> OnActionItem;
    public void Attach(BaseItem item)
    {
        _attachedItem = item;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if(_attachedItem is null) return; // TODO: Send another event request later
            if(_attachedItem is IUse useItem)
            {
                if(useItem.IsInUse) return;
                // useItem.Use();
                OnActionItem?.Invoke(_attachedItem, useItem.Use);
            }
        }
    }
}
