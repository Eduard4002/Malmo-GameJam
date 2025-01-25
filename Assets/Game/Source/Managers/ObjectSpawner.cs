using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject cauldronObject;


    public static ObjectSpawner instance;


    public int maxAmount = 9;

    public List<Object> objectsSpawned = new List<Object>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Debug.Log($"Spawning initial ingredients");
        SpawnNewIngredients(maxAmount);
    }

    public void SpawnNewIngredients(int numberOfIngredients)
    {
        int slotsFree = maxAmount - GridSystem.Instance.NumberOfSlotsTaken();
        int numberToSpawn = numberOfIngredients <= slotsFree ? numberOfIngredients : slotsFree;

        Debug.Log($"Spawning {numberOfIngredients} new ingredients");
        for(int i = 0; i < numberToSpawn; i++)
        {
            Debug.Log($"Spawning ingredient {i}");
            (int index, Vector2 pos) = GridSystem.Instance.FindEmptySlot();

            GameObject ingredient = Instantiate(objectPrefab, new Vector3(pos.x, pos.y, -2), Quaternion.identity);
            ingredient.GetComponent<Object>().SpawnIndex = index;
            ingredient.transform.parent = cauldronObject.transform;
            objectsSpawned.Add(ingredient.GetComponent<Object>());
        }
        bool validMoves = HasValidMoves();
        if(!validMoves){
            Debug.Log("No valid moves, spawning new ingredients");
            //Delete everything and start from beginning
            for(int i = 0; i < objectsSpawned.Count;i++){
                GridSystem.Instance.RemoveSlot(objectsSpawned[i].SpawnIndex);
                Destroy(objectsSpawned[i].gameObject);
                objectsSpawned.Clear();
            }
            SpawnNewIngredients(maxAmount);

        }
        Debug.Log("Game is playable: " + validMoves);
    }
    public bool HasValidMoves(){
        // Check all pairs of objects to see if at least one match exists
        for (int i = 0; i < objectsSpawned.Count; i++) {
            for (int j = i + 1; j < objectsSpawned.Count; j++) {
                // Compare characteristics of two different items
                if (objectsSpawned[i].Characteristics.Shape == objectsSpawned[j].Characteristics.Shape ||
                    objectsSpawned[i].Characteristics.Texture == objectsSpawned[j].Characteristics.Texture) {
                    // A valid move exists
                    return true;
                }
            }
        }

        // If no matches are found after checking all pairs
        return false;

    }
    public void RemoveObjectSpawned(int spawnIndex){
        //Find the object with the spawn index
        Object foundObject = objectsSpawned.Find(x => x.SpawnIndex == spawnIndex);
        objectsSpawned.Remove(foundObject);
    }

}
