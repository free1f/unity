using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using Unity.VisualScripting;
using Freelf.Item;
using UnityEngine;
using Freelf.Character.Interfaces;

namespace Freelf.Character
{
    public class CharacterItemAction : CharacterComponent
    {
        private BaseItem _attachedItem;
        public event Action<BaseItem, Action> OnActionItem;
        public void Attach(BaseItem item)
        {
            _attachedItem = item;
        }

        public override void Init() {}

        public void Interact(PressedInput input)
        {
            if (input.IsPressed)
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
}