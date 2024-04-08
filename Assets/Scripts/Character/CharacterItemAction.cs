using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using Unity.VisualScripting;
using Freelf.Item;
using UnityEngine;
using Freelf.Character.Interfaces;
using Freelf.Character.DataTransfer;

namespace Freelf.Character
{
    public class CharacterItemAction : CharacterComponent, ITick, IAttached<UseItemData>, IAttached<BaseItem>
    {
        private BaseItem _attachedItem;
        private UseItemData Data;

        public override void Init() {}

        private void Interact(PressedInput input)
        {
            if (input.IsPressed)
            {
                if(_attachedItem is null) return; // TODO: Send another event request later
                if(_attachedItem is IUse useItem)
                {
                    if(useItem.IsInUse) return;
                    // useItem.Use();
                    Data.OnActionItem?.Invoke(_attachedItem, useItem.Use);
                }
            }
        }

        public void Tick()
        {
            Interact(Data.input);
        }

        public void Attached(ref UseItemData value)
        {
            Data = value;
        }

        public void Attached(ref BaseItem value)
        {
            _attachedItem = value;
        }
    }
}