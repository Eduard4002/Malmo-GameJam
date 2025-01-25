using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[ExecuteInEditMode]
public class GridSystem : MonoBehaviour
{
    public Vector2[] spawnPositions;
    public List<Vector2> slotsTaken = new List<Vector2>();

    public static GridSystem instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }
    private void OnValidate() {
        spawnPositions = new Vector2[transform.childCount];
        for(int i = 0; i < transform.childCount; i++){
            spawnPositions[i] = transform.GetChild(i).transform.position;
        }
    }

    
    public Vector2 FindEmptySlot(){
        Vector2 position = Vector2.zero;
        //Loop through the spawnPositions
        //slotsTaken.Any(x => x.x != spawnPositions[randomIndex].x && x.y != spawnPositions[randomIndex].y)
        bool slotFound = false;
        while(!slotFound){
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Debug.Log($"Random index: {randomIndex}");
            if(slotsTaken.Count == 0 || slotsTaken.Any(x => x.x != spawnPositions[randomIndex].x && x.y != spawnPositions[randomIndex].y)){
                Debug.Log("Empty slot found");
                position = spawnPositions[randomIndex];
                slotsTaken.Add(position);
                Debug.Log("Slotstaken count" + slotsTaken.Count);
                slotFound = true;
            }
        }
        return position;
    }
    
}

