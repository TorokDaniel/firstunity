using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public float OpenSpeed = 10;
    public List<PressurePlate> PressurePlates;

    private int _activationCounter = 0;
    private bool _activated = false;

    // Use this for initialization
    private void Start ()
    {
        PressurePlates.ForEach(pressurePlate =>
        {
            pressurePlate.AddDelegate(delegate
            {
                _activationCounter += 1;
                _activated = _activationCounter >= PressurePlates.Count;
            });
        });
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
