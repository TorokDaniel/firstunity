using UnityEngine;

public class Action : MonoBehaviour, ISharedProximityLockDelegate
{

    public bool SingleUseOnly = true;
    
    private bool _alreadyUsed = false;
    private bool _inRange = false;
    private ISharedProximityLockHandler _sharedProximityLockHandler;

    private void Start()
    {
        _sharedProximityLockHandler = SharedProximityLock.Instance.Register(gameObject, this);
    }

    private void Update()
    {
        if (SingleUseOnly && _alreadyUsed)
        {
            return;
        }
        
        if (!_inRange || !Input.GetKeyDown(KeyCode.F))
        {
            return;
        }

        _alreadyUsed = true;
        OnAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        _sharedProximityLockHandler.AcquireLock();
    }

    private void OnTriggerExit(Collider other)
    {
        _sharedProximityLockHandler.ReleaseLock();
    }

    private void OnDestroy()
    {
        _sharedProximityLockHandler.Unregister();
    }

    public void OnLockAcquired()
    {
        _inRange = true;
    }

    public void OnLockLost()
    {
        _inRange = false;
    }
    
    protected virtual void OnAction()
    {
        
    }

    protected virtual void ResetAction()
    {
        _alreadyUsed = false;
    }

}