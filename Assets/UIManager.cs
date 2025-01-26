using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject pausePanel;
    public TMP_Text scoreText;
    private int score = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            UpdateScore(5);
        }
        else if (Input.GetKey(KeyCode.M))
        {
            UpdateScore(-5);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandeGamePauseAndResume();
        }
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = score.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void HandeGamePauseAndResume()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("ObjectSpawning");
    }
}
