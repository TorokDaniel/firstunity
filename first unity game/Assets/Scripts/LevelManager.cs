using UnityEngine.SceneManagement;

public class LevelManager: SingletonMonoBehaviour<LevelManager>
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(1);
    }
    
}