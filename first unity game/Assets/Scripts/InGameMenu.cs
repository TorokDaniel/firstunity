using UnityEngine;

public class InGameMenu: MonoBehaviour
{
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _canvas.enabled = !_canvas.enabled;
            Time.timeScale =  _canvas.enabled ? 0f : 1f;
        }
    }

    public void OnResumeButtonClicked()
    {
        _canvas.enabled = false;
        Time.timeScale = 1f;
    }

    public void OnRestartButtonClicked()
    {
        LevelManager.Instance.LoadTutorialScene();
    }

    public void OnExitToMenuButtonClicked()
    {
        LevelManager.Instance.LoadMainMenu();
    }

    public void OnExitToDesktopButtonClicked()
    {
        Application.Quit();
    }
    
}