using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Elements
{
    public abstract class BaseElement : MonoBehaviour
    {
        public abstract DataElement Data { get; protected set; }
    }
}