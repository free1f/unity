using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Item
{
    [CreateAssetMenu(fileName = "WeaponDataItem", menuName = "freelf/Item/WeaponData")]
    public class WeaponDataItem : DataItem
    {
        public int damage;
        public LayerMask allowedLayers;
        public int staminaCost;
    }
}

