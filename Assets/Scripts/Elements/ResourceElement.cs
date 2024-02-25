using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freelf.Elements.Interfaces;

namespace Freelf.Elements
{
    public class ResourceElement : BaseElement, IGather
    {

        [field: SerializeField]
        public override DataElement Data { get; protected set; }

        public void Gather()
        {
            Destroy(gameObject);
        }
    }
}
