using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public float OpenSpeed = 10;
    public List<PressurePlate> PressurePlates;
    
    private readonly Dictionary<PressurePlate, bool> _activations = new Dictionary<PressurePlate, bool>();
    private bool _activated = false;

    // Use this for initialization
    private void Start ()
    {
        PressurePlates.ForEach(pressurePlate =>
        {
            _activations.Add(pressurePlate, false);
            pressurePlate.AddDelegate(delegate
            {
                _activations[pressurePlate] = true;
                _activated = IsAllPressurePlateActivated();
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

    private bool IsAllPressurePlateActivated()
    {
        return _activations.Values.All(v => v);
    }

}
