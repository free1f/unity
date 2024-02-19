using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using UnityEngine;

public class CharacterHandler : MonoBehaviour, IInteract
{
    public CharacterAnimation characterAnimation;
    public CharacterMovement characterMovement;
    public CharacterCamera characterCamera;
    public CharacterStat characterStat;
    public Transform handPoint;
    public InventoryHandler inventoryHandler;
    public bool isDead = false;


    void Update()
    {
        if (isDead) return;
        if (characterAnimation.IsAnimationPaused) return;
        characterMovement.CalculateInput();
        characterMovement.CalculateMovement();
        characterAnimation.AnimateMotion(characterMovement.Direction.magnitude > 0);
        // characterAnimation.AnimateAction();
    }

    void LateUpdate()
    {
        characterCamera.CalculateCameraRotation();
    }

    private void OnEnable()
    {
        characterStat.OnDeath += Death;
        inventoryHandler.OnEquip += UseTool;
    }

    private void OnDisable()
    {
        characterStat.OnDeath -= Death;
        inventoryHandler.OnEquip -= UseTool;
    }

    private void Death()
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Faint"));
        isDead = true;
    }

    public void Interact(GameObject target)
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Picking up"));
        inventoryHandler.AddItem(target.GetComponent<BaseItem>(), handPoint);
    }

    private void UseTool(BaseItem item)
    {
        // TODO: Use methods from BaseItem to iteract with the world
        if(item == null) return;
        Debug.Log("Using " + item.name);
    }
}
