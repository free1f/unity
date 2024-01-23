using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int value;
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerStay(Collider other);

}
