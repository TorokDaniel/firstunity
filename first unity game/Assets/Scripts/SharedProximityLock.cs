using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISharedProximityLockDelegate
{
    void OnLockAcquired();
    void OnLockLost();
}

public interface ISharedProximityLockHandler
{
    void AcquireLock();
    void ReleaseLock();
    void Unregister();
}

internal class SharedProximityLockHandlerImpl : ISharedProximityLockHandler
{
    public delegate void Del();

    private readonly Del _del1;
    private readonly Del _del2;
    private readonly Del _del3;

    internal SharedProximityLockHandlerImpl(Del del1, Del del2, Del del3)
    {
        _del1 = del1;
        _del2 = del2;
        _del3 = del3;
    }
    
    public void AcquireLock()
    {
        _del1();
    }

    public void ReleaseLock()
    {
        _del2();
    }

    public void Unregister()
    {
        _del3();
    }
}

public class SharedProximityLock : SingletonMonoBehaviour<SharedProximityLock>
{
 
    private readonly Dictionary<GameObject, List<ISharedProximityLockDelegate>> _registeredDelegates = new Dictionary<GameObject, List<ISharedProximityLockDelegate>>();
    private readonly List<GameObject> _queuedGameObjects = new List<GameObject>();
    public Transform ProximityBase;

    private void Update()
    {
        if (_queuedGameObjects.Count < 2)
        {
            return;
        }

        const float threshold = 0.05f;
        var closestPoint = _queuedGameObjects.Min(_ => CalculateDistance(_));
        var closestGameObject = _queuedGameObjects.First(_ => Math.Abs(CalculateDistance(_) - closestPoint) < threshold);
        
        if (_queuedGameObjects[0] != closestGameObject)
        {
            _registeredDelegates[_queuedGameObjects[0]].ForEach(_ => _.OnLockLost());
            _queuedGameObjects.Remove(closestGameObject);
            _queuedGameObjects.Insert(0, closestGameObject);
            SetActive();
        }
    }

    private float CalculateDistance(GameObject targetGameObject)
    {
        var currentCollider = targetGameObject.GetComponent<Collider>();
        var closestPointOnBounds = currentCollider.ClosestPointOnBounds(ProximityBase.position);
        return Vector3.Distance(closestPointOnBounds, ProximityBase.position);
    }

    public ISharedProximityLockHandler Register(GameObject registeredGameObject, ISharedProximityLockDelegate @delegate)
    {
        if (!_registeredDelegates.ContainsKey(registeredGameObject))
        {
            _registeredDelegates.Add(registeredGameObject, new List<ISharedProximityLockDelegate>());
        }
        _registeredDelegates[registeredGameObject].Add(@delegate);
        
        return new SharedProximityLockHandlerImpl(
            () => AcquireLock(registeredGameObject),
            () => ReleaseLock(registeredGameObject),
            () =>
            {
                ReleaseLock(registeredGameObject);
                _registeredDelegates.Remove(registeredGameObject);
            });
    }

    private void AcquireLock(GameObject registeredGameObject)
    {
        if (!_registeredDelegates.ContainsKey(registeredGameObject) || _queuedGameObjects.Contains(registeredGameObject))
        {
            return;
        }
        
        _queuedGameObjects.Add(registeredGameObject);
        if (_queuedGameObjects.Count == 1)
        {
            SetActive();
        }
    }

    private void ReleaseLock(GameObject registeredGameObject)
    {
        var isActive = _queuedGameObjects.Count != 0 && _queuedGameObjects[0] == registeredGameObject;
        _queuedGameObjects.Remove(registeredGameObject);
        
        if (isActive)
        {
            _registeredDelegates[registeredGameObject].ForEach(_ => _.OnLockLost());
            SetActive();
        }
    }

    private void SetActive()
    {
        if (_queuedGameObjects.Count > 0)
        {
            _registeredDelegates[_queuedGameObjects[0]].ForEach(_ => _.OnLockAcquired());
        }
    }
    
}
