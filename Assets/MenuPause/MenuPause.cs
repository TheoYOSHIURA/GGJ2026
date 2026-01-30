using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;

    public static bool isPaused;

    void Start()
    {
        PauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
        public void QuitGame()
        {
            Application.Quit();

        }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

        public void ResumeGame()
        {
        PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
}