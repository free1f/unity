using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Elements
{
    [CreateAssetMenu(fileName = "DataElement", menuName = "freelf/Element/DataElement")]
    public class DataElement : ScriptableObject
    {
        public string elementName;
        public string description;
        public int level;
        public ElementType elementType;
    }
}