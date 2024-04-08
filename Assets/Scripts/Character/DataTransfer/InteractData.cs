using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item;
using UnityEngine;


namespace Freelf.Character.DataTransfer
{
    public class InteractData
    {
        public PressedInput input;
        public Action<BaseItem> OnInteract;
    }
}
