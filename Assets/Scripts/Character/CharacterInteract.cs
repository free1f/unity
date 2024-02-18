using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    public float rayDistance = 2f;
    public float rayRadius = 0.5f;
    public Vector3 rayOffset = Vector3.zero;
    public Transform body;
    public LayerMask allowedLayers;
    private bool _isInteracting;
    private float _interactingCooldown = 0.5f;

    private void Update()
    {
        // _isInteracting = Input.GetKey(KeyCode.E);

        if (Input.GetKey(KeyCode.E) && _interactingCooldown <= 0)
        {
            _isInteracting = true;
            _interactingCooldown = 0.5f;
        }
        else
        {
            _isInteracting = false;
            _interactingCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // RaycastHit hit;
        // if (Physics.SphereCast(body.position, rayRadius, transform.forward, out hit)) 
        // {
        //     Debug.Log(hit.collider.name);
        //     if (hit.collider.TryGetComponent<ToolItem>(out var toolItem))
        //     {
        //         toolItem.PickUp();
        //     }
        // }

        if (!_isInteracting) return;

        List<ToolItem> toolItems = new (); 
        var colliders = Physics.OverlapSphere(body.position + rayOffset, rayRadius, allowedLayers);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<ToolItem>(out var toolItem))
            {
                toolItems.Add(toolItem);
            }
        }

        if (toolItems.Count == 0) return;
        toolItems.Sort((a, b) => Vector3.Distance(a.transform.position, body.position).CompareTo(Vector3.Distance(b.transform.position, body.position)));
        toolItems[0].Pickup();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(body.position, rayRadius);
    }
}
