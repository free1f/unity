using System;
using System.Collections;
using System.Collections.Generic;
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
        characterAnimation.AnimateAction();
    }

    void LateUpdate()
    {
        characterCamera.CalculateCameraRotation();
    }

    private void OnEnable()
    {
        characterStat.OnDeath += Death;
    }

    private void OnDisable()
    {
        characterStat.OnDeath -= Death;
    }

    private void Death()
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Faint"));
        isDead = true;
    }

    public void Interact(GameObject target)
    {
        StartCoroutine(characterAnimation.WaitForAnimation("Picking up"));
        target.transform.SetParent(handPoint);
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
        target.GetComponent<Rigidbody>().isKinematic = true;
        inventoryHandler.AddItem(target.GetComponent<BaseItem>());
    }
}
