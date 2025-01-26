using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    public Vector3 upPosition;
    public Vector3 downPosition; 

    private Vector3 moveTowards;
    public float speed;
    public GameObject backgroundObject;

    private bool startAnimation = false;

    public static UIManager instance;

    public EventReference startGameSound;
    public EventReference resumeGameSound;
    public EventReference buttonSound;

    public Number scoreSystem;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update() {
        if(!startAnimation) return;

        backgroundObject.transform.position = Vector3.MoveTowards(backgroundObject.transform.position, moveTowards, speed * Time.deltaTime );

        if(backgroundObject.transform.position == moveTowards) startAnimation = false;

        
        if(backgroundObject.transform.position == downPosition){
            GameManager.instance.StartGame();
        }
    }

    public void StartAnimation(bool goingDown){

        if (goingDown)
        {
            AudioManager.Instance.PlayOneShot(startGameSound);
        }
        else
        {
            AudioManager.Instance.PlayOneShot(buttonSound);
        }
        startAnimation = true;

        moveTowards = goingDown == true ? downPosition : upPosition;

        Cursor.instance.ToggleWand(goingDown);
    }


    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;

        int tempScore = int.Parse(score.ToString("D7"));
        scoreSystem.UpdateNumber(tempScore);
    }

    public int GetScore()
    {
        return score;
    }

    public void RestartGame()
    {
        AudioManager.Instance.PlayOneShot(resumeGameSound);
        SceneManager.LoadSceneAsync("ObjectSpawning");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayOneShot(buttonSound);
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
