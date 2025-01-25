using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public static ObjectSelection instance;
    public List<Object> itemsSelected;
    public Object lastObjectSelected;

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
        lastObjectSelected = null;
    }

    public void IngredientSelected(Object selection){
        if(itemsSelected.Contains(selection)) return;
        if(itemsSelected.Count == 0){
            //Start a new timer 
            timerStarted = true;
            timeLeft = timerAmount;
        }
        itemsSelected.Add(selection);

    }
    public void IngredientClicked(Object ingredient){
        if(itemsSelected.Count == 1) return;
         //User has double clicked on the same item, selection has finished
        if(lastObjectSelected == ingredient){
            timerStarted = false;
            timeLeft = timerAmount;

            //Calculate the score based on the items selected

            //Destroy the items
            for(int i = 0; i < itemsSelected.Count;i++){
                Destroy(itemsSelected[i].gameObject);
            }
            // Clear the list after destroying items
            itemsSelected.Clear();
        }
        lastObjectSelected = ingredient;
    }


}
