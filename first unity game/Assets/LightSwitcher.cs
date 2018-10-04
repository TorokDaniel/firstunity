using UnityEngine;

public class LightSwitcher : MonoBehaviour
{

	public PressurePlate PressurePlate;
	private Light _light;

	// Use this for initialization
	private void Start ()
	{
		_light = GetComponent<Light>();
		PressurePlate.AddDelegate(delegate { _light.enabled = false; });
	}

}
