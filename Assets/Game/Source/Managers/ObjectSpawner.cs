using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject cauldronObject;


    public static ObjectSpawner instance;


    public int maxAmount = 9;
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


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewIngredients(int numberOfIngredients)
    {
        int slotsFree = maxAmount - GridSystem.instance.NumberOfSlotsTaken();
        int numberToSpawn = numberOfIngredients <= slotsFree ? numberOfIngredients : slotsFree;

        Debug.Log($"Spawning {numberOfIngredients} new ingredients");
        for(int i = 0; i < numberOfIngredients; i++)
        {
            Debug.Log($"Spawning ingredient {i}");
            (int index, Vector2 pos) = GridSystem.instance.FindEmptySlot();

            GameObject ingredient = Instantiate(objectPrefab, new Vector3(pos.x, pos.y, -2), Quaternion.identity);
            ingredient.GetComponent<Object>().SpawnIndex = index;
            ingredient.transform.parent = cauldronObject.transform;
        }
    }

}
