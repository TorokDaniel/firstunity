using UnityEngine;

public class Action : MonoBehaviour
{

    private bool _inRange = false;

    private void Start()
    {
        var exclusiveMonoBehaviour = GetComponent<ExclusiveMonoBehaviour>();
        exclusiveMonoBehaviour.Register(OnExclusiveTriggerEnter, OnExclusiveTriggerExit);
    }

    private void Update()
    {
        if (_inRange && Input.GetKeyDown(KeyCode.F))
        {
            OnAction();
        }
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
    
}