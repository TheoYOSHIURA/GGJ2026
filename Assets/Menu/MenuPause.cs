using UnityEngine;

using UnityEngine.Playables;

public class MenuPause : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject container;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playableDirector.Play();

            container.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
        public void MainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
        public void QuitGame()
        {
            Application.Quit();

        }

        public void ResumeGame()
        {
        container.SetActive(false);
        Time.timeScale = 1;
    }
}