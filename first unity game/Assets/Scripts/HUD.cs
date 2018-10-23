using UnityEngine;

public class HUD: MonoBehaviour
{
    public static HUD Instance { get; private set; }

    public static void Destroy()
    {
        if (Instance != null)
        {
            
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    
}