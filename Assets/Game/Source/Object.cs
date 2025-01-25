using UnityEngine;
using UnityEngine.EventSystems;

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

    private bool itemSelected;
    private GameObject bubbbleOverlay;


    public void Awake()
    {
        Characteristics = GenerateCharacteristics();
        bubbbleOverlay = transform.GetChild(0).gameObject;
        bubbbleOverlay.SetActive(false);
    }

    public CharacteristicDefinition GenerateCharacteristics()
    {
        int randomIndex = Random.Range(0, GameManager.Characteristics.Count);
        return GameManager.Characteristics[randomIndex];
    }

    public void ToggleBubble(){
        //The user has clicked on the object, either select/deselect
        itemSelected = !itemSelected;

        bubbbleOverlay.SetActive(itemSelected);

        if(itemSelected){
            ObjectSelection.instance.IngredientSelected(this);
        }

    }

}
