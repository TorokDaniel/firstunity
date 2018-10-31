using UnityEngine;

namespace Inventory
{
    public class InventoryItem: MonoBehaviour
    {
        public string Id;
        public float PickUpRadius = 1.5f;
        public bool PickUpByPassing = false;

        private bool _inRange = false;

        private void Start()
        {
            if (PickUpByPassing)
            {
                GetComponent<Collider>().isTrigger = true;
            }
        
            var inRangeCollider = gameObject.AddComponent<SphereCollider>();
            inRangeCollider.isTrigger = true;
            inRangeCollider.radius = PickUpRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PickUpByPassing)
            {
                PickUp();
                return;
            }
        
            _inRange = true;
            HUDScreen.Instance.PickupItem(Id);            
        }

        private void OnTriggerExit(Collider other)
        {
            if (PickUpByPassing)
            {
                return;            
            }
        
            _inRange = false;
            HUDScreen.Instance.DismissPickupItem();
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
            global::Inventory.Inventory.Instance.AddItem(Id);
            HUDScreen.Instance.DismissPickupItem();
            Destroy(gameObject);
        }
    
    }
}