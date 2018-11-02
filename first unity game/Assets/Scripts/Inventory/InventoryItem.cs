using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem: MonoBehaviour
    {
        public string Id;
        public bool PickUpByPassing = false;

        private bool _inRange = false;
        private GameObject _displayedHUDHint;

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
            ShowHUDHint();            
        }

        private void OnTriggerExit(Collider other)
        {
            if (PickUpByPassing)
            {
                return;            
            }
        
            _inRange = false;
            DismissHUDHint();
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
            DismissHUDHint();
            Destroy(gameObject);
        }

        private void ShowHUDHint()
        {
            _displayedHUDHint = Instantiate(Inventory.Instance.ItemHUDHint);
            _displayedHUDHint.transform.Find("item_name_text").GetComponent<Text>().text = Id;
            var deltaPosition = new Vector3(0, 1, 0);
            _displayedHUDHint.transform.position = transform.position + deltaPosition;
        }

        private void DismissHUDHint()
        {
            Destroy(_displayedHUDHint);
            _displayedHUDHint = null;
        }
    
    }
}