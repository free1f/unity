using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Item
{
    public class DataItem : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public float weight;
        public float cost;
        public bool isStackable;
        public int maxStack;
    }
}