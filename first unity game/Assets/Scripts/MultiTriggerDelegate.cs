using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiTriggerDelegate : MonoBehaviour, ITriggerDelegate
{
    public List<SingleTriggerDelegate> SceneTriggerDelegates = new List<SingleTriggerDelegate>();
    
    private readonly List<TriggerDelegate> _codeTriggerDelegates = new List<TriggerDelegate>();
    private readonly Dictionary<ITriggerDelegate, bool> _activations = new Dictionary<ITriggerDelegate, bool>();

    private void Start()
    {
        SceneTriggerDelegates.ForEach(@delegate =>
        {
            _activations[@delegate] = false;
            @delegate.AddDelegate(OnSceneTriggerDelegateCalled);
        });
    }

    public void AddDelegate(TriggerDelegate @delegate)
    {
        _codeTriggerDelegates.Add(@delegate);
    }

    private void OnSceneTriggerDelegateCalled(ITriggerDelegate @delegate, Collider @delegateCollider)
    {
        if (_activations[@delegate])
        {
            return;
        }
        
        _activations[@delegate] = true;
        if (IsAllSceneTriggerDelegateActivated())
        {
            CallCodeTriggerDelegates();
        }
    }

    private bool IsAllSceneTriggerDelegateActivated()
    {
        return _activations.Values.All(value => value);
    }

    private void CallCodeTriggerDelegates()
    {
        _codeTriggerDelegates.ForEach(@delegate => @delegate(this, null));
    }
}
