using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public static ObjectSelection instance;
    public List<Object> itemsSelected;

    [SerializeField, Tooltip("How long can the user select (seconds)")] float timerAmount = 10f;
    private bool timerStarted;
    private float timeLeft;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }
    private void Update() {
        if(timerStarted){
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0){
                timerStarted = false;
                //destroy the bubbles
                DestroyBubbles();
            }
        } 
    }
    private void DestroyBubbles(){
        for(int i = 0; i < itemsSelected.Count;i++){
            itemsSelected[i].ToggleBubble();
        }
        itemsSelected.Clear();
    }

    public void IngredientSelected(Object selection){
        if(itemsSelected.Count == 0){
            //Start a new timer 
            timerStarted = true;
            timeLeft = timerAmount;
        }
        itemsSelected.Add(selection);
    }

}
