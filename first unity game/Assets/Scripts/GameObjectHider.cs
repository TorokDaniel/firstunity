using UnityEngine;

public class GameObjectHider : MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;

    private void Start()
    {
        TriggerDelegate.AddDelegate(_ => GetComponent<Renderer>().enabled = false);
    }
}
