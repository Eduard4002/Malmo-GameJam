using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static List<CharacteristicDefinition> Characteristics;
    public Transform cursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite[] plainSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-blue-filled");
        Sprite[] zigzagSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-purple-zigzag"); //currently unused
        Sprite[] stripedSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-red-striped");
        Sprite[] dotsSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-yellow-dots");

        //0 = frog
        //1 = fish
        //2 = skull
        //3 = chickenfeet
        //4 = eyeball
        //5 = mushroom

        Characteristics = new List<CharacteristicDefinition>()
        {
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Plain, Sprite = plainSpriteArray[0] },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Spots, Sprite = dotsSpriteArray[0] },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Stripes, Sprite = stripedSpriteArray[0] },

            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Plain, Sprite = plainSpriteArray[4] },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Spots, Sprite = dotsSpriteArray[4] },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Stripes, Sprite = stripedSpriteArray[4] },

            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Plain, Sprite = plainSpriteArray[3] },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Spots, Sprite = dotsSpriteArray[3] },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Stripes, Sprite = stripedSpriteArray[3] },
        };
    }

    // Update is called once per frame
    void Update()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.position = mousePos;

	}
}
