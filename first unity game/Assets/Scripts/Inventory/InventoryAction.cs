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
                OnUse.Invoke(gameObject);
                HUDScreen.Instance.DismissPickupItem();
                RemoveUsedItems();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _inRange = true;
            HUDScreen.Instance.PickupItem("PRESS 'F' WHEN YOU HAVE: " + GenerateRequiredItemText());
        }

        private void OnTriggerExit(Collider other)
        {
            _inRange = false;
            HUDScreen.Instance.DismissPickupItem();
        }

        private string GenerateRequiredItemText()
        {
            var formattedItemTexts = _requiredItems.Select(item => string.Format("{0} ({1})", item.Key, item.Value));
            var concatenatedItemTexts = string.Join(", ", formattedItemTexts.ToArray());
            return concatenatedItemTexts == "" ? "Nothing." : concatenatedItemTexts;
        }

        private bool HasAllItems()
        {
            var inventoryItems = global::Inventory.Inventory.Instance.Items;
            return _requiredItems.All(item => inventoryItems.ContainsKey(item.Key) && inventoryItems[item.Key] >= item.Value);
        }

        private void RemoveUsedItems()
        {
            foreach (var item in _requiredItems)
            {
                var id = item.Key;
                var quantity = item.Value;
                global::Inventory.Inventory.Instance.RemoveItem(id, quantity);
            }
        }
    
    }
}