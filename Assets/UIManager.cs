using FMODUnity;
using TMPro;
using UnityEngine;
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

	private void Start()
	{
        Music.Instance.SetTarget(0);
	}

	private void Update() {
        if(!startAnimation) return;

        backgroundObject.transform.position = Vector3.MoveTowards(backgroundObject.transform.position, moveTowards, speed * Time.deltaTime );

        if(backgroundObject.transform.position == moveTowards) startAnimation = false;

        
        if(backgroundObject.transform.position == downPosition){
            GameManager.instance.StartGame();
        }
        if(backgroundObject.transform.position == upPosition){
            GameManager.instance.StopGame();
            SceneManager.LoadSceneAsync("ObjectSpawning");

        }
    }

    public void StartAnimation(bool goingDown){

        if (goingDown)
		{
			Music.Instance.SetTarget(1);
			AudioManager.Instance.PlayOneShot(startGameSound);
        }
        else
		{
			Music.Instance.SetTarget(0);
			AudioManager.Instance.PlayOneShot(buttonSound);
        }
        startAnimation = true;

        moveTowards = goingDown == true ? downPosition : upPosition;

        Cursor.instance.ToggleWand(goingDown);
    }


    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        if (score < 0)
            score = 0; 

        scoreSystem.UpdateNumber(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void RestartGame()
    {
        /* Debug.Log("Called restart game");
        GameManager.instance.StopGame();
        Debug.Log("Stopped game");
        GameManager.instance.StartGame();
        Debug.Log("started game"); */
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
