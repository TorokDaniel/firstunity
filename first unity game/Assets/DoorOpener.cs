﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    public float openSpeed = 10;
    public PressurePlate switchButton;
    bool activated = false;

	// Use this for initialization
	void Start () {
        switchButton.triggerDelegate = delegate () { activated = true; };
    }
	
	// Update is called once per frame
	void Update () {
        if (!activated)
        {
            return;
        }

		if (transform.eulerAngles.y < 90)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * openSpeed);
        }
	}

}
