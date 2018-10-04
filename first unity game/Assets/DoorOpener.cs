using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public float OpenSpeed = 10;
    public PressurePlate PressurePlate;
    private bool _activated = false;

    // Use this for initialization
    private void Start ()
    {
        PressurePlate.AddDelegate(delegate { _activated = true; });
    }

    // Update is called once per frame
    private void Update ()
    {
        if (!_activated)
        {
            return;
        }

        if (transform.eulerAngles.y < 90)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * OpenSpeed);
        }
    }

}
