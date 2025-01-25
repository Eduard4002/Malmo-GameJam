using UnityEngine;
public enum Shape
{
    Frog,
    Eye,
    Chickenfeet
}

public enum Texture
{
    Plain,
    Spots,
    Stripes
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
    private GameObject bubbbleOverlay;

    public void Awake()
    {
        Characteristics = GenerateCharacteristics();
        bubbbleOverlay = transform.GetChild(0).gameObject;
        bubbbleOverlay.SetActive(false);
        sr = GetComponentInChildren<SpriteRenderer>();
		sr.sprite = Characteristics.Sprite;
        sr.color = Color.clear;
    }

	private void Start()
	{
        transform.name = $"{Characteristics.Shape} - {Characteristics.Texture}";
    }

	private void Update()
	{
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime * 2);
	}

	public CharacteristicDefinition GenerateCharacteristics()
    {
        int randomIndex = Random.Range(0, GameManager.Characteristics.Count);
        return GameManager.Characteristics[randomIndex];
    }
}
