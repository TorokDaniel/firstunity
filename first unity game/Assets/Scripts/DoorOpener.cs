using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float OpenSpeed = 25;
    public MultiTriggerDelegate TriggerDelegate;
    
    private bool _activated = false;

    private void Start()
    {
        TriggerDelegate.AddDelegate(@delegate => _activated = true);
    }

    private void Update()
    {
        if (!_activated)
        {
            return;
        }

        if (transform.localEulerAngles.y < 90)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * OpenSpeed);
        }
        else
        {
            _activated = false;
        }
    }
}
