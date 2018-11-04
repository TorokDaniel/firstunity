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
        internal GameObject ReferenceGameObject;
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
    private bool IsCurrentlyActive
    {
        get { return _activeExclusiveTrigger != null && _activeExclusiveTrigger.Id == _registeredExclusiveTrigger.Id; }
    }
    
    public void Register(ExclusiveTrigger onEnter, ExclusiveTrigger onExit)
    {
        _registeredExclusiveTrigger.OnEnter = WrapExclusiveTrigger(_registeredExclusiveTrigger.OnEnter, onEnter);
        _registeredExclusiveTrigger.OnExit = WrapExclusiveTrigger(_registeredExclusiveTrigger.OnExit, onExit);
    }

    private static ExclusiveTriggerHolder ClosestExclusiveTriggerToPosition(Vector3 referencePosition)
    {
        const float threshold = 0.02f;
        var minReturnValue = _activeExclusiveTrigger;
        var minDistance = Vector3.Distance(referencePosition, _activeExclusiveTrigger.ReferenceGameObject.GetComponent<Collider>().ClosestPointOnBounds(referencePosition));
        
        foreach (var trigger in EnqueuedExclusiveTriggers.ToArray())
        {
            var comparePosition = trigger.ReferenceGameObject.GetComponent<Collider>().ClosestPointOnBounds(referencePosition);
            var compareDistance = Vector3.Distance(referencePosition, comparePosition);
            if (compareDistance < minDistance && minDistance - compareDistance >= threshold)
            {
                minReturnValue = trigger;
                minDistance = compareDistance;
            }
        }

        return minReturnValue;
    }

    private void Update()
    {
        if (!IsCurrentlyActive || EnqueuedExclusiveTriggers.Count == 0)
        {
            return;
        }

        var minDistanceRegisteredTrigger = ClosestExclusiveTriggerToPosition(_activeExclusiveTrigger.OnEnterCollider.gameObject.transform.position);
        if (minDistanceRegisteredTrigger.Id != _activeExclusiveTrigger.Id)
        {
            //Debug.Log("change: " + _activeExclusiveTrigger.ReferenceGameObject.name + " => " + minDistanceRegisteredTrigger.ReferenceGameObject.name);
            _activeExclusiveTrigger.OnExit(null);
            RemoveExclusiveTrigger(_activeExclusiveTrigger);
            EnqueuedExclusiveTriggers.Add(_activeExclusiveTrigger);
            
            _activeExclusiveTrigger = minDistanceRegisteredTrigger;
            RemoveExclusiveTrigger(minDistanceRegisteredTrigger);
            minDistanceRegisteredTrigger.OnEnter(minDistanceRegisteredTrigger.OnEnterCollider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _registeredExclusiveTrigger.OnEnterCollider = other;
        _registeredExclusiveTrigger.ReferenceGameObject = gameObject;
        
        RemoveExclusiveTrigger(_registeredExclusiveTrigger);
        EnqueuedExclusiveTriggers.Add(_registeredExclusiveTrigger);
        if (_activeExclusiveTrigger == null)
        {
            _activeExclusiveTrigger = GetNextExclusiveTrigger();
            CallNextExclusiveTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveExclusiveTrigger(_registeredExclusiveTrigger);
        if (_activeExclusiveTrigger.Id == _registeredExclusiveTrigger.Id)
        {
            _activeExclusiveTrigger.OnExit(other);
            _activeExclusiveTrigger = GetNextExclusiveTrigger();
            CallNextExclusiveTrigger();
        }
    }

    private static void RemoveExclusiveTrigger(ExclusiveTriggerHolder trigger)
    {
        var triggerPairIndex = EnqueuedExclusiveTriggers.FindIndex(currentTrigger => currentTrigger.Id == trigger.Id);
        if (triggerPairIndex >= 0)
        {
            EnqueuedExclusiveTriggers.RemoveAt(triggerPairIndex);            
        }
    }

    private void OnDestroy()
    {
        if (_activeExclusiveTrigger == null || _registeredExclusiveTrigger == null)
        {
            return;
        }
        
        if (_activeExclusiveTrigger.Id == _registeredExclusiveTrigger.Id)
        {
            _activeExclusiveTrigger = GetNextExclusiveTrigger();
            CallNextExclusiveTrigger();
        }
        else
        {
            RemoveExclusiveTrigger(_registeredExclusiveTrigger);
        }
    }
    
}