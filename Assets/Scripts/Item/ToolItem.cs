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
        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactable.Interact(gameObject);
        }
    }
}