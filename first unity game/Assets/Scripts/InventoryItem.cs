using UnityEngine;
using UnityEngine.Serialization;

public class InventoryItem: MonoBehaviour
{
    public string Id;
    public float PickUpRange = 4;

    private bool _inRange = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        // TODO: find a way to dynamically extend the "radius"
        //GetComponent<Collider>().contactOffset = PickUpRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        _inRange = true;
        HUDScreen.Instance.PickupItem(Id);
        
    }

    private void OnTriggerExit(Collider other)
    {
        _inRange = false;
        HUDScreen.Instance.DismissPickupItem();
    }

    private void Update()
    {
        if (!_inRange)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Inventory.Instance.AddItem(Id);
            HUDScreen.Instance.DismissPickupItem();
            Destroy(gameObject);
        }
    }
    
}