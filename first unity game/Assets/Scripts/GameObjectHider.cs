using UnityEngine;

public class GameObjectHider : MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;

    private void Start()
    {
        TriggerDelegate.AddDelegate(delegate(ITriggerDelegate @delegate)
        {
            var objectRenderer = GetComponent<Renderer>();
            objectRenderer.enabled = false;
        });
    }
}
