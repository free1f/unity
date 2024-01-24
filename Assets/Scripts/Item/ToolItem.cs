using UnityEngine;

public class ToolItem : BaseItem
{
    private IInteract _interactable;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteract interactable))
        {
            _interactable = interactable;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (_interactable == null) return;
        if (Input.GetKey(KeyCode.E))
        {
            _interactable.Interact(gameObject);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteract interactable))
        {
            _interactable = null;
        }
    }
}