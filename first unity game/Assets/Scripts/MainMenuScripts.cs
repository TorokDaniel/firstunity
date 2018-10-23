using UnityEngine;

public class MainMenuScripts: MonoBehaviour
{
    public void OnNewGameButtonClicked()
    {
        LevelManager.Instance.LoadTutorialScene();
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
    
}