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

    public void Awake()
    {
        Characteristics = GenerateCharacteristics();
    }

    public CharacteristicDefinition GenerateCharacteristics()
    {
        int randomIndex = Random.Range(0, 9);
        return GameManager.Characteristics[randomIndex];
    }

}
