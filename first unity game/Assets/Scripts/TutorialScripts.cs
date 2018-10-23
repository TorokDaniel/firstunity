using UnityEngine;

public class TutorialScripts: MonoBehaviour
{
    public SingleTriggerDelegate TriggerDelegate;
    
    private void Start()
    {
        TriggerDelegate.AddDelegate((_, __) => LevelManager.Instance.LoadMainMenu());
    }
    
}