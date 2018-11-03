using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryAction : MonoBehaviour
    {

        [Serializable]
        public class ItemDefinition
        {
            public InventoryItem InventoryItem;
            [Range(1, 100)] public int RequiredQuantity;
        }
    
        [Serializable]
        public class GameObjectEvent : UnityEvent<GameObject>
        {

        }

        public ItemDefinition[] RequiredItems;
        public GameObjectEvent OnUse;

        private bool _inRange = false;
        private readonly Dictionary<string, int> _requiredItems = new Dictionary<string, int>();

        public virtual void OnUseMethod()
        {
            
        }

        private void Start()
        {
            foreach (var item in RequiredItems)
            {
                _requiredItems.Add(item.InventoryItem.Id, item.RequiredQuantity);
            }
        }

        private void Update()
        {
            if (!_inRange || !Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            if (HasAllItems())
            {
                OnUseMethod();
                if (OnUse != null)
                {
                    OnUse.Invoke(gameObject);                    
                }
                
                RemoveUsedItems();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _inRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _inRange = false;
            HUDScreen.Instance.DismissPickupItem();
        }

        private bool HasAllItems()
        {
            var inventoryItems = Inventory.Instance.Items;
            return _requiredItems.All(item => inventoryItems.ContainsKey(item.Key) && inventoryItems[item.Key] >= item.Value);
        }

        private void RemoveUsedItems()
        {
            foreach (var item in _requiredItems)
            {
                var id = item.Key;
                var quantity = item.Value;
                Inventory.Instance.RemoveItem(id, quantity);
            }
        }
    
    }
}