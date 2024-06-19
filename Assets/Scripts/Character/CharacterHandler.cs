using System;
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
        private readonly List<CharacterComponent> characterComponents = new ();
        
        [Header("Character Extras")]
        public Transform handPoint;
        public InventoryHandler inventoryHandler;
        public bool isDead = false;
        
        // Data Transfers
        private MovementData movementData;
        private AnimationData animationData;
        private JumpData jumpData;
        private UseItemData useItemData;
        private InteractData interactData;
        private DamageData damageData;
        private CameraData cameraData;
        private StatData statData;

        private IStatUpdater statUpdater;
        #endregion

        private void Awake()
        {
            animationData = new ();
            movementData = new ();
            jumpData = new ();
            useItemData = new ();
            interactData = new ();
            damageData = new ();
            cameraData = new();
            statData = new();
            
            characterComponents.AddRange(GetComponents<CharacterComponent>());
            
            foreach (var component in characterComponents)
            {
                (component as IAttached<AnimationData>)?.Attached(ref animationData);
                (component as IAttached<MovementData>)?.Attached(ref movementData);
                (component as IAttached<JumpData>)?.Attached(ref jumpData);
                (component as IAttached<UseItemData>)?.Attached(ref useItemData);
                (component as IAttached<InteractData>)?.Attached(ref interactData);
                (component as IAttached<DamageData>)?.Attached(ref damageData);
                (component as IAttached<CameraData>)?.Attached(ref cameraData);
                (component as IAttached<StatData>)?.Attached(ref statData);
            }

            if (characterComponents.Find(component => component is IStatUpdater) is not IStatUpdater foundComponent)
                return;
            statUpdater = foundComponent;
        }

        private void Start()
        {
            foreach (var component in characterComponents)
            {
                component.Init();
            }
            
            Subscribe();
        }
        
        private void FixedUpdate()
        {
            foreach (var component in characterComponents)
            {
                (component as IFixedTick)?.FixedTick();
            }
        }

        private void Update()
        {
            if (isDead) return;
            
            foreach (var component in characterComponents)
            {
                (component as IPreTick)?.PreTick();
            }
            
            animationData.isMoving = movementData.direction.magnitude > 0;
            useItemData.CurrentStamina = statData.CurrentStamina;  // Move to an event when Stamina changes
            
            foreach (var component in characterComponents)
            {
                if (animationData.isAnimationPaused) return;
                (component as ITick)?.Tick();
            }
        }

        private void LateUpdate()
        {
            foreach (var component in characterComponents)
            {
                (component as ILateTick)?.LateTick();
            }
        }
        
        // Unity Methods for Subscriptions
        private void Subscribe()
        {
            statData.OnEmptyHealth += Death;
            interactData.OnInteract += Interact;
            useItemData.OnActionItem += Action;
            inventoryHandler.OnEquip += Equip;

            if (statUpdater == null) return;
            damageData.OnDamage += statUpdater.SetHealth;
            useItemData.OnUseStamina += statUpdater.SetStamina;
        }

        private void OnDestroy()
        {
            statData.OnEmptyHealth -= Death;
            interactData.OnInteract -= Interact;
            useItemData.OnActionItem -= Action;
            inventoryHandler.OnEquip -= Equip;

            if (statUpdater == null) return;
            damageData.OnDamage -= statUpdater.SetHealth;
            useItemData.OnUseStamina -= statUpdater.SetStamina;
        }
        
        private void Death()
        {
            StartCoroutine(animationData.WaitForAnimation("Faint"));
            isDead = true;
        }

        private void Interact(BaseItem item)
        {
            if (!item.TryGetComponent<IPickup>(out var itemPickup)) return;
            StartCoroutine(animationData.WaitForAnimation("Picking up"));
            itemPickup.Pickup();
            inventoryHandler.AddItem(item, handPoint);
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
            StartCoroutine(animationData.WaitForAnimation(item.Data.actionData.animationName, item.Data.actionData.animationWaitTime, actionCallback));
        }
    }
}