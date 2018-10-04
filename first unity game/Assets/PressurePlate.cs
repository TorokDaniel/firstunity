using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    internal delegate void Delegate();
    private readonly List<Delegate> _triggerDelegates = new List<Delegate>();

    internal void AddDelegate(Delegate @delegate)
    {
        _triggerDelegates.Add(@delegate);
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerDelegates.ForEach(@delegate => { @delegate(); });
    }

}
