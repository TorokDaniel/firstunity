using System.Collections.Generic;
using UnityEngine;

public class ExclusiveMonoBehaviour : MonoBehaviour
{
    public delegate void ExclusiveTrigger(Collider other);

    private class ExclusiveTriggerHolder
    {
        internal readonly string Id = System.Guid.NewGuid().ToString();
        internal ExclusiveTrigger OnEnter = _ => {};
        internal ExclusiveTrigger OnExit = _=> {};
        internal Collider OnEnterCollider;
        internal GameObject GameObject;
    }
    
    private static ExclusiveTrigger WrapExclusiveTrigger(ExclusiveTrigger original, ExclusiveTrigger proxy)
    {
        return (other) =>
        {
            original(other);
            proxy(other);
        };
    }
    
    private static ExclusiveTriggerHolder GetNextExclusiveTrigger()
    {
        if (EnqueuedExclusiveTriggers.Count == 0)
        {
            return null;
        }

        var nextTrigger = EnqueuedExclusiveTriggers[0];
        EnqueuedExclusiveTriggers.RemoveAt(0);
        return nextTrigger;
    }

    private static void CallNextExclusiveTrigger()
    {
        if (_activeExclusiveTrigger == null)
        {
            return;
        }

        _activeExclusiveTrigger.OnEnter(_activeExclusiveTrigger.OnEnterCollider);
    }
    
    private static readonly List<ExclusiveTriggerHolder> EnqueuedExclusiveTriggers = new List<ExclusiveTriggerHolder>();
    private static ExclusiveTriggerHolder _activeExclusiveTrigger;

    private readonly ExclusiveTriggerHolder _registeredExclusiveTrigger = new ExclusiveTriggerHolder();
    
    public void Register(ExclusiveTrigger onEnter, ExclusiveTrigger onExit)
    {
        _registeredExclusiveTrigger.OnEnter = WrapExclusiveTrigger(_registeredExclusiveTrigger.OnEnter, onEnter);
        _registeredExclusiveTrigger.OnExit = WrapExclusiveTrigger(_registeredExclusiveTrigger.OnExit, onExit);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _registeredExclusiveTrigger.GameObject = gameObject;
        _registeredExclusiveTrigger.OnEnterCollider = other;
        EnqueuedExclusiveTriggers.Add(_registeredExclusiveTrigger);
        
        if (_activeExclusiveTrigger == null || _activeExclusiveTrigger.GameObject == null)
        {
            _activeExclusiveTrigger = GetNextExclusiveTrigger();
            CallNextExclusiveTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_activeExclusiveTrigger.Id == _registeredExclusiveTrigger.Id)
        {
            _activeExclusiveTrigger.OnExit(other);
            _activeExclusiveTrigger = GetNextExclusiveTrigger();
            CallNextExclusiveTrigger();
        }
        else
        {
            var triggerPairIndex = EnqueuedExclusiveTriggers.FindIndex(trigger => trigger.Id == _registeredExclusiveTrigger.Id);
            EnqueuedExclusiveTriggers.RemoveAt(triggerPairIndex);
        }
    }
    
}