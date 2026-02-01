using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour


{
    [SerializeField] GameObject ControlMenu;

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
    }
    public void MainMenuButton()
    {
        ControlMenu.SetActive(false);
    }
}