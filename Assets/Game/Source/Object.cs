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
    private bool itemSelected;
    private GameObject bubbbleOverlay;


    public void Awake()
    {
        Characteristics = GenerateCharacteristics();
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Characteristics.Sprite;
        bubbbleOverlay = transform.GetChild(0).gameObject;
        bubbbleOverlay.SetActive(false);
    }

	private void Start()
	{
        transform.name = $"{Characteristics.Shape} - {Characteristics.Texture}";

    }

	public CharacteristicDefinition GenerateCharacteristics()
    {
        int randomIndex = Random.Range(0, GameManager.Characteristics.Count);
        return GameManager.Characteristics[randomIndex];
    }
    public void ToggleSelection(){
        itemSelected = !itemSelected;

        bubbbleOverlay.SetActive(itemSelected);
    }


    public void ToggleBubble(){
        ToggleSelection();
        if(itemSelected){
            ObjectSelection.instance.IngredientSelected(this);
        }

        ObjectSelection.instance.IngredientClicked(this);

    }

}
