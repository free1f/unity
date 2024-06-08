using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item.Interfaces;
using Freelf.Inventory;
using Freelf.Item;
using UnityEngine;
using Freelf.Character.Interfaces;
using Freelf.Character.DataTransfer;

namespace Freelf.Character
{
    public class CharacterHandler : MonoBehaviour
    {
        #region Global Variables
        [Header("Character Subsystems")]
        private List<CharacterComponent> characterComponents = new ();
        public CharacterInput characterInput;
        public CharacterCamera characterCamera;
        public CharacterStat characterStat;
        [Header("Character Extras")]
        public Transform handPoint;
        public InventoryHandler inventoryHandler;
        private MovementData movementData;
        private AnimationData animationData;
        private JumpData jumpData;
        private UseItemData useItemData;
        private InteractData interactData;
        private DamageData damageData;

        public bool isDead = false;
        #endregion
        void Start()
        {
            animationData = new ();
            movementData = new ();
            jumpData = new ();
            useItemData = new ();
            interactData = new ();
            damageData = new ();
            characterComponents.AddRange(GetComponents<CharacterComponent>());
            foreach (var component in characterComponents)
            {
                (component as IAttached<AnimationData>)?.Attached(ref animationData);
                (component as IAttached<MovementData>)?.Attached(ref movementData);
                (component as IAttached<JumpData>)?.Attached(ref jumpData);
                (component as IAttached<UseItemData>)?.Attached(ref useItemData);
                (component as IAttached<InteractData>)?.Attached(ref interactData);
                (component as IAttached<DamageData>)?.Attached(ref damageData);
                component?.Init();
            }
            Suscribe();
        }
        
        private void FixedUpdate()
        {
            foreach (var component in characterComponents)
            {
                (component as IFixedTick)?.FixedTick();
            }
        }

        void Update()
        {
            if (isDead) return;
            characterInput.GetJumpInput(ref jumpData);
            characterInput.GetMovementInput(ref movementData);
            characterInput.GetUseItemInput(ref useItemData);
            characterInput.GetInteractInput(ref interactData);
            animationData.isMoving = movementData.direction.magnitude > 0;
            useItemData.CurrentStamina = characterStat.CurrentStamina;  // Move to an event when Stamina changes
            
            foreach (var component in characterComponents)
            {
                if ((component as IAnimationData)?.IsAnimationPaused ?? false) return;
                (component as ITick)?.Tick();
            }
        }

        void LateUpdate()
        {
            characterCamera.CalculateCameraRotation();
        }
        
        // Unity Methods for Subscriptions
        private void Suscribe()
        {
            characterStat.OnDeath += Death;
            interactData.OnInteract += Interact;
            useItemData.OnActionItem += Action;
            inventoryHandler.OnEquip += Equip;
            useItemData.OnUseStamina += characterStat.SetStamina;
        }

        private void OnDestroy()
        {
            characterStat.OnDeath -= Death;
            interactData.OnInteract -= Interact;
            useItemData.OnActionItem -= Action;
            inventoryHandler.OnEquip -= Equip;
            useItemData.OnUseStamina -= characterStat.SetStamina;
        }

        private void Death()
        {
            StartCoroutine(animationData.WaitForAnimation("Faint"));
            isDead = true;
        }

        private void Interact(BaseItem item)
        {
            if (item.TryGetComponent<IPickup>(out var itemPickup))
            {
                StartCoroutine(animationData.WaitForAnimation("Picking up"));
                itemPickup.Pickup();
                inventoryHandler.AddItem(item, handPoint);
            }
        }

        private void Equip(BaseItem item)
        {
            // TODO: Use methods from BaseItem to iteract with the world
            foreach (var component in characterComponents)
            {
                (component as IAttached<BaseItem>)?.Attached(ref item);
            }
            
        }

        private void Action(BaseItem item, Action actionCallback)
        {
            Debug.Log("Action");
            if(item == null) return;
            StartCoroutine(animationData.WaitForAnimation(item.Data.actionData.animationName, actionCallback));
        }
    }
}