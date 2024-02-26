using System.Collections;
using System.Collections.Generic;
using Freelf.Elements;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolDataItem", menuName = "freelf/Item/ToolData")]
public class ToolDataItem : DataItem
{
    public ElementType[] compatibleElements; // TODO: Change to ScriptableObject
    public LayerMask allowedLayers;
    public int level;
}
