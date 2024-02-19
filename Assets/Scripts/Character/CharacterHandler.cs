using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public CharacterAnimation characterAnimation;
    public CharacterMovement characterMovement;
    public CharacterCamera characterCamera;
    public CharacterInteract characterInteract;
    public CharacterStat characterStat;
    public Transform handPoint;
    public InventoryHandler inventoryHandler;
    public bool isDead = false;
    
    private void FixedUpdate()
    {
        characterInteract.WaitToInteract();
    }

    void Update()
    {
        if (isDead) return;
        if (characterAnimation.IsAnimationPaused) return;
        characterMovement.CalculateInput();
        characterMovement.CalculateMovement();
        characterAnimation.AnimateMotion(characterMovement.Direction.magnitude > 0);
    }

    void LateUpdate()
    {
        characterCamera.CalculateCameraRotation();
    }
    
    // Unity Methods for Subscriptions
    private void OnEnable()
    {
        characterStat.OnDeath += Death;
        characterInteract.OnInteract += Interact;
        characterInteract.OnUse += Use;
        // inventoryHandler.OnEquip += UseTool;
    }

    private void OnDisable()
    {
        characterStat.OnDeath -= Death;
        characterInteract.OnInteract -= Interact;
        characterInteract.OnUse -= Use;
        // inventoryHandler.OnEquip -= UseTool;
    }

    private void Death()
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Faint"));
        isDead = true;
    }

    private void Interact(BaseItem item)
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Picking up"));

        if (item.TryGetComponent<IPickup>(out var itemPickup))
        {
            itemPickup.Pickup();
            inventoryHandler.AddItem(item, handPoint);
        }
    }

    private void UseTool(BaseItem item)
    {
        // TODO: Use methods from BaseItem to iteract with the world
        if(item == null) return;
        Debug.Log("Using " + item.name);
    }

    private void Use()
    {
        var item = inventoryHandler.CurrentItemInUse();
        if(item == null) return; // We can add some logic here to use empty hands

        (item as IUse)?.Use();
    }
}
