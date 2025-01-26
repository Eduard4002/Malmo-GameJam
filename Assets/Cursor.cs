using UnityEngine;

public class Cursor : MonoBehaviour
{
    public static Cursor instance;
    public WandPillow wandPillow;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;

        //The wand only turns on after the user starts the game
        ToggleWand(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Transform>().position = mousePos;
    }

    public void ToggleWand(bool toggle){
        transform.GetChild(0).gameObject.SetActive(toggle);
        UnityEngine.Cursor.visible = toggle ? false : true;

        if (wandPillow)
            wandPillow.ToggleWand(!toggle);
    }
}

