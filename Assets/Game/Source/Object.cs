using UnityEngine;
public enum Shape
{
    Frog,
    Fish,
    Mushroom,
    Chickenfeet,
    Eye,
    Skull,
}

public enum Texture
{
    Plain,
    Spots,
    Stripes,
    ZigZag
}

public struct CharacteristicDefinition
{
    public Shape Shape;
    public Texture Texture;
    public Sprite Sprite;
}

public class Object : MonoBehaviour
{
    public CharacteristicDefinition Characteristics;
    public int SpawnIndex;
	public SpriteRenderer sr;

    private Shakeable shake;
    private GameObject bubbbleOverlay;
    private bool isDeleted = false;

	public void Awake()
    {
        Characteristics = GenerateCharacteristics();
        
        bubbbleOverlay = transform.GetChild(0).gameObject;
        bubbbleOverlay.SetActive(false);

        sr = GetComponentInChildren<SpriteRenderer>();
		sr.sprite = Characteristics.Sprite;
        sr.color = Color.clear;

        shake = GetComponentInChildren<Shakeable>();
    }

	private void Start()
	{
        transform.name = $"{Characteristics.Shape} - {Characteristics.Texture}";
        NewMoveTowardsPoint();
    }

	private void Update()
	{
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime * 2);
        if (isDeleted)
            DeleteAnimation();
	}

    Vector2 moveTowards;
	private void FixedUpdate() {
        if(isDeleted) return;
        
        transform.position = Vector3.MoveTowards(transform.position, moveTowards, 0.15f * Time.fixedDeltaTime);
        Debug.DrawRay(moveTowards, Vector2.up);

		if (Vector2.Distance(transform.position, moveTowards) < 0.2f)
        {
            NewMoveTowardsPoint();
        }
	}
    private void NewMoveTowardsPoint()
    {
		float range = .6f;
		float randX = Random.Range(-range, range);
		float randY = Random.Range(-range, range);
		Vector2 spawnPosition = GridSystem.instance.spawnPositions[SpawnIndex].position;
		moveTowards = new Vector2(spawnPosition.x + randX, spawnPosition.y + randY);
	}

	public CharacteristicDefinition GenerateCharacteristics()
    {
        int randomIndex = Random.Range(0, GameManager.Characteristics.Count);
        return GameManager.Characteristics[randomIndex];
    }

    public void SetCharacteristics(CharacteristicDefinition characteristic)
    {
        this.Characteristics = characteristic;

        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = Characteristics.Sprite;
        sr.color = Color.clear;
    }

    public void Fail()
    {
        shake.InduceStress(1.2f);
	}

	public void DeleteSelf()
	{
        isDeleted = true;
        t = Random.Range(0.0f, 0.7f);
        riseSpeed += Random.Range(-0.15f, 0.35f);
        bubbbleOverlay.SetActive(true);
        Destroy(gameObject, 10f);
        sr.sortingOrder = 1000;
	}

    float t = 0;
	float riseSpeed = 1;
    public void DeleteAnimation()
    {
        // magic numbers are my fav
        t += Time.deltaTime;
        float x = Mathf.Sin(t*3) * Time.deltaTime;
        float y = Mathf.Lerp(.1f, 1f, t) * Time.deltaTime * riseSpeed;

        transform.position += new Vector3(x*.5f, y, 0);
    }
}
