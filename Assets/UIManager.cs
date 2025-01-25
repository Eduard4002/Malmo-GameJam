using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public TMP_Text scoreText;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
