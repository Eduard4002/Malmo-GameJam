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

    public void SpawnNewIngredients()
    {
        int slotsFree = maxAmount - GridSystem.instance.NumberOfSlotsTaken();

        Debug.Log($"Spawning {slotsFree} new ingredients");
        for(int i = 0; i < slotsFree; i++)
        {
            Debug.Log($"Spawning ingredient {i}");
            SpawnSingleIngredient();
        }
        bool validMoves = HasValidMoves();
        if(!validMoves){
            Debug.Log("No valid moves, spawning new ingredients");
            //Delete everything and start from beginning
            for(int i = 0; i < objectsSpawned.Count;i++){
                GridSystem.instance.RemoveSlot(objectsSpawned[i].SpawnIndex);
                Destroy(objectsSpawned[i].gameObject);
                objectsSpawned.Clear();
            }
            SpawnNewIngredients();

        }
        Debug.Log("Game is playable: " + validMoves);
    }

    private void SpawnSingleIngredient(CharacteristicDefinition characteristic)
    {
        (int index, Vector2 pos) = GridSystem.instance.FindEmptySlot();

        GameObject ingredient = Instantiate(objectPrefab, new Vector3(pos.x, pos.y, -2), Quaternion.identity);
        ingredient.GetComponent<Object>().SpawnIndex = index;
        ingredient.GetComponent<Object>().SetCharacteristics(characteristic);
        ingredient.transform.parent = cauldronObject.transform;
        objectsSpawned.Add(ingredient.GetComponent<Object>());
    }

    private void SpawnSingleIngredient()
    {
        (int index, Vector2 pos) = GridSystem.instance.FindEmptySlot();

        GameObject ingredient = Instantiate(objectPrefab, new Vector3(pos.x, pos.y, -2), Quaternion.identity);
        ingredient.GetComponent<Object>().SpawnIndex = index;
        ingredient.transform.parent = cauldronObject.transform;
        objectsSpawned.Add(ingredient.GetComponent<Object>());
    }

    public void SpawnStarterIngredients(StarterStage stage)
    {
        List<CharacteristicDefinition> characteristics;
        switch (stage)
        {
            case StarterStage.Stage1:
                characteristics =  GameManager.instance.GetCharacteristicsForStage(stage);
                SpawnSingleIngredient(characteristics[0]);
                SpawnSingleIngredient(characteristics[0]);
                break;
            case StarterStage.Stage2:
                characteristics = GameManager.instance.GetCharacteristicsForStage(stage);
                SpawnSingleIngredient(characteristics[0]);
                SpawnSingleIngredient(characteristics[1]);
                break;
            case StarterStage.Stage3:
            default:
                characteristics = GameManager.instance.GetCharacteristicsForStage(stage);
                SpawnSingleIngredient(characteristics[0]);
                SpawnSingleIngredient(characteristics[1]);
                SpawnSingleIngredient(characteristics[2]);
                break;
        }
        GameManager.instance.IncrementGameStarterStage();
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

    /// <summary>
    /// Changes the max amount, but includes a cap of how many are possible, due to the soawn positions being hardcoded
    /// </summary>
    public void ChangeMaxAmount(int newMaxAmount)
    {
        maxAmount = newMaxAmount <= GridSystem.instance.NumberOfSpawnPositions() - 5 ? newMaxAmount : GridSystem.instance.NumberOfSpawnPositions();
    }

}

public enum StarterStage
{
    Stage1,
    Stage2,
    Stage3,
    Other
}
