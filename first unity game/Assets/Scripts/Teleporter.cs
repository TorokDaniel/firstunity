using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;
    public GameObject Destination;
    public Vector3 DestinationOffset = new Vector3(2, 0, 0);
    public Vector3 DestinationAngle = new Vector3(0, 90, 0);
    
    private void Start()
    {
        TriggerDelegate.AddDelegate((_, delegateCollider) =>
        {
            var destinationTransform = Destination.GetComponent<Transform>();
            var colliderTransform = delegateCollider.gameObject.GetComponent<Transform>();

            colliderTransform.position = destinationTransform.position + DestinationOffset;
            colliderTransform.localEulerAngles = DestinationAngle;
        });
    }
}