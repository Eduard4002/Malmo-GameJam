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
        startAnimation = true;

        moveTowards = goingDown == true ? downPosition : upPosition;

        Cursor.instance.ToggleWand(goingDown);
    }


    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("ObjectSpawning");
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
