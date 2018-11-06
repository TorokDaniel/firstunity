using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HUDHint : MonoBehaviour, ISharedProximityLockDelegate
{

    public GameObject HUDHintPrefab;
    public Transform ReferencePosition;
    public Vector3 DeltaPosition = new Vector3(0, 0, 0);
    public string Text;

    public GameObject OnSceneHudHint { get; private set; }
    private ISharedProximityLockHandler _sharedProximityLockHandler;

    private void Start()
    {
        _sharedProximityLockHandler = SharedProximityLock.Instance.Register(gameObject, this);
    }

    private void Update()
    {
        if (OnSceneHudHint == null)
        {
            return;
        }
        
        OnSceneHudHint.transform.position = ReferencePosition.position + DeltaPosition;
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
        OnSceneHudHint = Instantiate(HUDHintPrefab);
        OnSceneHudHint.name = name + "_HUDHint";
        SetTextOnHudHint();
    }

    public void OnLockLost()
    {
        Destroy(OnSceneHudHint);
        OnSceneHudHint = null;
    }
    
    public void UpdateText(string text)
    {
        Text = text;
        SetTextOnHudHint();
    }
    
    private void SetTextOnHudHint()
    {
        OnSceneHudHint.transform.Find("text").GetComponent<Text>().text = TransformText(Text);
    }

    protected virtual string TransformText(string text)
    {
        return text;
    }
    
}