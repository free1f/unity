using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Item
{
    [CreateAssetMenu(fileName = "ToolDataItem", menuName = "freelf/Item/ToolData")]
    public class ToolDataItem : DataItem
    {
        public string[] allowedTags;
    }
}

