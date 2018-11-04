using UnityEngine;

public class Action : MonoBehaviour
{

    public bool SingleUseOnly = true;
    
    private bool _alreadyUsed = false;
    private bool _inRange = false;

    private void Start()
    {
        var exclusiveMonoBehaviour = GetComponent<ExclusiveMonoBehaviour>();
        exclusiveMonoBehaviour.Register(OnExclusiveTriggerEnter, OnExclusiveTriggerExit);
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

    private void OnExclusiveTriggerEnter(Collider other)
    {
        _inRange = true;
    }

    private void OnExclusiveTriggerExit(Collider other)
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