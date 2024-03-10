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
            Debug.Log($"Gathering {Data.elementName}");
            int resourceRate = Random.Range(1, Data.maxResourceRate);
            for (int i = 0; i < resourceRate; i++)
            {
                int randomResource = Random.Range(0, Data.resources.Length);
                Instantiate(Data.resources[randomResource], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
