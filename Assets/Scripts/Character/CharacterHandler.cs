using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using Freelf.Inventory;
using Freelf.Item;
using UnityEngine;
using Freelf.Character.Interfaces;

namespace Freelf.Character
{
    public class CharacterHandler : MonoBehaviour
    {
        [Header("Character Subsystems")]
        private List<CharacterComponent> characterComponents;
        public CharacterAnimation characterAnimation;
        public CharacterMovement characterMovement;
        public CharacterJump characterJump;
        public CharacterInput characterInput;
        public CharacterCamera characterCamera;
        public CharacterInteract characterInteract;
        public CharacterStat characterStat;
        public CharacterItemAction characterItemAction;
        [Header("Character Extras")]
        public Transform handPoint;
        public InventoryHandler inventoryHandler;

        public bool isDead = false;

        void Awake()
        {
            characterComponents.AddRange(GetComponents<CharacterComponent>());
            foreach (var component in characterComponents)
            {
                component.Init();
            }
        }
        
        private void FixedUpdate()
        {
            characterInteract.WaitToInteract(characterInput.GetInteractInput());
        }

        void Update()
        {
            if (isDead) return;
            if (characterAnimation.IsAnimationPaused) return;
            characterMovement.PerformMovement(characterInput.GetMovementInput());
            characterMovement.CalculateMovement();
            characterJump.CheckGround();
            characterJump.CalculateJump();
            characterJump.PerformJump(characterInput.GetJumpInput());
            characterItemAction.Interact(characterInput.GetUseItemInput());
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
            characterItemAction.OnActionItem += Action;
            inventoryHandler.OnEquip += Equip;
        }

        private void OnDisable()
        {
            characterStat.OnDeath -= Death;
            characterInteract.OnInteract -= Interact;
            characterItemAction.OnActionItem -= Action;
            inventoryHandler.OnEquip -= Equip;
        }

        private void Death()
        {
            StartCoroutine(characterAnimation.WaitForAnimation("Faint"));
            isDead = true;
        }

        private void Interact(BaseItem item)
        {
            if (item.TryGetComponent<IPickup>(out var itemPickup))
            {
                StartCoroutine(characterAnimation.WaitForAnimation("Picking up"));
                itemPickup.Pickup();
                inventoryHandler.AddItem(item, handPoint);
            }
        }

        private void Equip(BaseItem item)
        {
            // TODO: Use methods from BaseItem to iteract with the world
            characterItemAction.Attach(item);
            
        }

        private void Action(BaseItem item, Action actionCallback)
        {
            Debug.Log("Action");
            if(item == null) return;
            StartCoroutine(characterAnimation.WaitForAnimation(item.Data.actionData.animationName, actionCallback));
        }
    }
}