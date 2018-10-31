using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class Inventory: SingletonMonoBehaviour<Inventory>
    {

        public GameObject MainCharacter;

        public override void SingletonStart()
        {
            var colliderToMove = GetComponent<SphereCollider>();
            var newCollider = MainCharacter.AddComponent<SphereCollider>();
            newCollider.isTrigger = colliderToMove.isTrigger;
            newCollider.radius = colliderToMove.radius;
            newCollider.center = colliderToMove.center;
            
            Destroy(colliderToMove);
        }

        private readonly Dictionary<string, int> _items = new Dictionary<string, int>();
        public Dictionary<string, int> Items
        {
            get { return _items.ToDictionary(e => e.Key, e => e.Value); }
        }

        public void AddItem(string id, int quantity = 1)
        {
            if (!_items.ContainsKey(id))
            {
                _items[id] = 0;
            }

            _items[id] = _items[id] + quantity;
        }

        public void RemoveItem(string id, int quantity = 1)
        {
            if (!_items.ContainsKey(id))
            {
                return;
            }
        
            _items[id] -= quantity;
            if (_items[id] <= 0)
            {
                _items.Remove(id);
            }
        }
    
    }
}