using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Item
{
    [DisallowMultipleComponent]
    public abstract class BaseItem : MonoBehaviour
    {
        public abstract DataItem Data { get; protected set; }
        public abstract void Info();
        // protected abstract void OnTriggerEnter(Collider other);
        // protected abstract void OnTriggerStay(Collider other);
        // protected abstract void OnTriggerExit(Collider other);
    }
}