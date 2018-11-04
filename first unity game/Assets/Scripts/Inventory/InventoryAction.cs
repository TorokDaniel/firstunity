using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryAction : Action
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
       
        public virtual void OnUseMethod()
        {
            
        }

        protected override void OnAction()
        {
            if (!HasAllItems())
            {
                ResetAction();
                return;
            }

            OnUseMethod();
            if (OnUse != null)
            {
                OnUse.Invoke(gameObject);                    
            }
            
            RemoveUsedItems();
        }

        private bool HasAllItems()
        {
            var inventoryItems = Inventory.Instance.Items;
            return RequiredItems.All(item => inventoryItems.ContainsKey(item.InventoryItem.Id) && inventoryItems[item.InventoryItem.Id] >= item.RequiredQuantity);
        }

        private void RemoveUsedItems()
        {
            foreach (var item in RequiredItems)
            {
                Inventory.Instance.RemoveItem(item.InventoryItem.Id, item.RequiredQuantity);
            }
        }
    
    }
}