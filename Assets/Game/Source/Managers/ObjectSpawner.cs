using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject cauldronObject;


    public int maxAmount = 9;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            Debug.Log("starting to spawn"); 
        for(int i = 0; i < maxAmount;i++){
            Debug.Log($"Filling empty slot: {i}");
            Vector2 pos = GridSystem.instance.FindEmptySlot();

           

            GameObject temp = Instantiate(objectPrefab, new Vector3(pos.x, pos.y, -2), Quaternion.identity);
            temp.transform.parent = cauldronObject.transform;


        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
