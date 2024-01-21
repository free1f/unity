using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tool")) 
        {
            Debug.Log($"Interacted with {other.gameObject.name}");
        }
    }
}
