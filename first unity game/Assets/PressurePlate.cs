using UnityEngine;

public class PressurePlate : MonoBehaviour {

    internal delegate void Delegate();
    internal Delegate TriggerDelegate;

    private void OnTriggerEnter(Collider other)
    {
        TriggerDelegate();
    }

}
