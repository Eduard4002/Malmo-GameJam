using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{

    public Vector3 endPosition; 
    public float speed;
    public GameObject backgroundObject;

    private bool startAnimation = false;
    private void Update() {
        if(!startAnimation) return;
        backgroundObject.transform.position = Vector3.MoveTowards(backgroundObject.transform.position, endPosition, speed * Time.deltaTime );

        if(backgroundObject.transform.position == endPosition){
            //Start the game
            SceneManager.LoadSceneAsync("ObjectSpawning");
        }
    }
    
    public void StartGame()
    {
        startAnimation = true;
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }

}
