using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("ObjectSpawning");
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
