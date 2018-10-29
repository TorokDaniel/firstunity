using UnityEngine;

public abstract class SingletonMonoBehaviour<T>: MonoBehaviour where T: class
{
    public static T Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
            SingletonStart();
        }
        else
        {
            Destroy(this);
        }
    }

    public virtual void SingletonStart()
    {
        
    }
    
}