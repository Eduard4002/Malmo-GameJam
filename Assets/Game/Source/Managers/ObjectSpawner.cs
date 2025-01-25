using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject cauldronObject;


    public int maxAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < maxAmount;i++){
            Vector3 pos = RandomPointInCircle(cauldronObject.transform.position, cauldronObject.transform.localScale.x / 4f);

           

            GameObject temp = Instantiate(objectPrefab, pos, Quaternion.identity);
            temp.transform.parent = cauldronObject.transform;


        }
    }
    Vector3 RandomPointInCircle (Vector3 center, float radius){
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

}
