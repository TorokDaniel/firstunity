using UnityEngine;

public class LightSwitcher : MonoBehaviour
{

	public SingleTriggerDelegate SingleTriggerDelegate;
	private Light _light;

	// Use this for initialization
	private void Start ()
	{
		_light = GetComponent<Light>();
		SingleTriggerDelegate.AddDelegate(delegate { _light.enabled = false; });
	}

}
