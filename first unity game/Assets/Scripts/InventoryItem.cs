using UnityEngine;
using UnityEngine.Serialization;

public class InventoryItem: MonoBehaviour
{
    public string Id;
    public float PickUpRadius = 1.5f;

    private bool _inRange = false;

    private void Start()
    {
        var inRangeCollider = gameObject.AddComponent<SphereCollider>();
        inRangeCollider.isTrigger = true;
        inRangeCollider.radius = PickUpRadius;
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
        if (!_inRange || !Input.GetKeyDown(KeyCode.F))
        {
            return;
        }
        
        Inventory.Instance.AddItem(Id);
        HUDScreen.Instance.DismissPickupItem();
        Destroy(gameObject);
    }
    
}