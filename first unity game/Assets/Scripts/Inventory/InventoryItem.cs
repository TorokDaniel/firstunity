using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem: MonoBehaviour
    {
        public string Id;
        public bool PickUpByPassing = false;
        private bool _inRange = false;

        private void Start()
        {
            if (PickUpByPassing)
            {
                GetComponent<Collider>().isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PickUpByPassing)
            {
                PickUp();
                return;
            }
        
            _inRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (PickUpByPassing)
            {
                return;            
            }
        
            _inRange = false;;
        }

        private void Update()
        {
            if (PickUpByPassing)
            {
                return;
            }
            if (!_inRange || !Input.GetKeyDown(KeyCode.F))
            {
                return;
            }
        
            PickUp();
        }

        private void PickUp()
        {
            Inventory.Instance.AddItem(Id);
            Destroy(gameObject);
        }
    
    }
}