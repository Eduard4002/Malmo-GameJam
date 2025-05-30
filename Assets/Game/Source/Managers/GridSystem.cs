using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
[ExecuteInEditMode]
public class GridSystem : MonoBehaviour
{
    public Transform[] spawnPositions;
    public List<int> slotsTaken = new List<int>();

    public static GridSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnValidate() {
        spawnPositions = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++){
            spawnPositions[i] = transform.GetChild(i).transform;
        }
    }

    public (int, Vector2) FindEmptySlot(){
        Vector2 finalPos = Vector2.zero;
        int randomIndex = -1;
        if (slotsTaken.Count == 0) {
            randomIndex = Random.Range(0, spawnPositions.Length);
            finalPos = spawnPositions[randomIndex].position;
            slotsTaken.Add(randomIndex);
        }else{
            bool slotFound = false;
            while(!slotFound){
                randomIndex = Random.Range(0, spawnPositions.Length);
                if(!slotsTaken.Contains(randomIndex)){
                    finalPos = spawnPositions[randomIndex].position;
                    slotsTaken.Add(randomIndex);
                    slotFound = true;
                    break;
                }

            }
        }

        return (randomIndex,finalPos);
    }

    public void RemoveSlot(int index)
    {
        slotsTaken.Remove(index);
    }

    public int NumberOfSlotsTaken()
    {
        return slotsTaken.Count;
    }
    
    public int NumberOfSpawnPositions()
    {
        return spawnPositions.Length;
    }
}

