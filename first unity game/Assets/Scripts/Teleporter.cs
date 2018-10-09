using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;
    public GameObject Destination;
    public Vector3 DestinationOffset = new Vector3(2, 0, 0);
    public Vector3 DestinationAngle = new Vector3(0, 90, 0);
    
    private void Start()
    {
        TriggerDelegate.AddDelegate(OnTriggerActivated);
    }

    private void OnTriggerActivated(ITriggerDelegate triggerDelegate, Collider triggerCollider)
    {
        var destinationTransform = Destination.GetComponent<Transform>();
        var colliderTransform = triggerCollider.gameObject.GetComponent<Transform>();

        colliderTransform.position = destinationTransform.position + DestinationOffset;
        colliderTransform.localEulerAngles = DestinationAngle;
    }
}