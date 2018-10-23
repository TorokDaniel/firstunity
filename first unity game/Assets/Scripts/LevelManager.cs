using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager: MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(1);
    }
    
}