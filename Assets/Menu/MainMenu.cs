using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour


{
    [SerializeField] GameObject ControlMenu;
    [SerializeField] GameObject _mainMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    public void ControlsButton()
    {
        ControlMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }
    public void MainMenuButton()
    {
        ControlMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Menu");
    }
}