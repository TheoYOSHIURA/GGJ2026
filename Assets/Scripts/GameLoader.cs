using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Menu");
    }
}
