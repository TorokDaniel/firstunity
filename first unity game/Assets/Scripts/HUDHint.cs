using UnityEngine;
using UnityEngine.UI;

public class HUDHint : MonoBehaviour
{

    public GameObject HUDHintPrefab;
    public Transform ReferencePosition;
    public Vector3 DeltaPosition = new Vector3(0, 0, 0);
    public string Text;

    private GameObject _onSceneHUDHint;

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
        if (_onSceneHUDHint == null)
        {
            return;
        }
        
        _onSceneHUDHint.transform.position = ReferencePosition.position + DeltaPosition;
    }

    private void OnExclusiveTriggerEnter(Collider other)
    {
        _onSceneHUDHint = Instantiate(HUDHintPrefab);
        SetTextOnHUDHint();
    }

    private void OnExclusiveTriggerExit(Collider other)
    {
        Destroy(_onSceneHUDHint);
        _onSceneHUDHint = null;
    }

    private void OnDestroy()
    {
        if (_onSceneHUDHint != null)
        {
            Destroy(_onSceneHUDHint);
        }
    }

    private void SetTextOnHUDHint()
    {
        _onSceneHUDHint.transform.Find("text").GetComponent<Text>().text = Text;
    }
    
}