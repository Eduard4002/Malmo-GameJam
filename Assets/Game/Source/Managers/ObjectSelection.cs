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

            //See if the user has the correct selection
            bool succeded = CheckSuccession(itemsSelected);
            Debug.Log("Correct selection: " + succeded);
            if(succeded){
                //Calculate the score based on the items selected

                //Destroy the items only if we succeded
                for(int i = 0; i < itemsSelected.Count;i++){
                    Destroy(itemsSelected[i].gameObject);
                }
            }else{
                for(int i = 0; i < itemsSelected.Count;i++){
                    itemsSelected[i].ToggleSelection();
                }
                itemsSelected[itemsSelected.Count - 1].ToggleSelection();
            }
            // Clear the list
            itemsSelected.Clear();
        }
        lastObjectSelected = ingredient;
    }
    private bool CheckSuccession(List<Object> items){
        if(items == null || items.Count == 0) return false;
        bool success = true;
        Object check = itemsSelected[0];
        //Check if the user has picked the correct or wrong ingredients
        for(int i = 0; i < itemsSelected.Count;i++){
            if(itemsSelected[i].Characteristics.Shape == check.Characteristics.Shape ||itemsSelected[i].Characteristics.Texture == check.Characteristics.Texture){
                continue;
            }else{
                success = false;
                break;
            }
        }
        return success;

    }


}
