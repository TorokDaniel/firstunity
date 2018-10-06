using System.Collections.Generic;
using UnityEngine;

public class SingleTriggerDelegate : MonoBehaviour, ITriggerDelegate
{
    private readonly List<TriggerDelegate> _triggerDelegates = new List<TriggerDelegate>();
    
    public void AddDelegate(TriggerDelegate @delegate)
    {
        _triggerDelegates.Add(@delegate);
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerDelegates.ForEach(@delegate => @delegate(this));
    }
}
