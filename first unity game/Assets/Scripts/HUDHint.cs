using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HUDHint : MonoBehaviour
{

    public GameObject HUDHintPrefab;
    public Transform ReferencePosition;
    public Vector3 DeltaPosition = new Vector3(0, 0, 0);
    public string Text;

    internal GameObject OnSceneHudHint;

    public void UpdateText(string text)
    {
        Text = text;
        SetTextOnHUDHint();
    }

    private void Start()
    {
        var exclusiveMonoBehaviour = GetComponent<ExclusiveMonoBehaviour>();
        exclusiveMonoBehaviour.Register(OnExclusiveTriggerEnter, OnExclusiveTriggerExit);
    }

    private void Update()
    {
        if (OnSceneHudHint == null)
        {
            return;
        }
        
        OnSceneHudHint.transform.position = ReferencePosition.position + DeltaPosition;
    }

    private void OnExclusiveTriggerEnter(Collider other)
    {
        OnSceneHudHint = Instantiate(HUDHintPrefab);
        SetTextOnHUDHint();
    }

    private void OnExclusiveTriggerExit(Collider other)
    {
        Destroy(OnSceneHudHint);
        OnSceneHudHint = null;
    }

    private void OnDestroy()
    {
        if (OnSceneHudHint != null)
        {
            Destroy(OnSceneHudHint);
        }
    }

    private void SetTextOnHUDHint()
    {
        OnSceneHudHint.transform.Find("text").GetComponent<Text>().text = Text;
    }
    
}