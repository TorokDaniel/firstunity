using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;
    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
        TriggerDelegate.AddDelegate((_, __) => _light.enabled = !_light.enabled);
    }
}
