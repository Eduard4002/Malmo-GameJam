using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static List<CharacteristicDefinition> Characteristics;
    //public Transform cursor;

    public static GameManager instance;

    public bool gameStarted;

    public StarterStage currentStage;

    Sprite[] plainSpriteArray;
    Sprite[] zigzagSpriteArray;
    Sprite[] stripedSpriteArray;
    Sprite[] dotsSpriteArray;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plainSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-blue-filled");
        zigzagSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-purple-zigzag"); //currently unused
        stripedSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-red-striped");
        dotsSpriteArray = Resources.LoadAll<Sprite>("Sprites/Ingredients/Ingredients-yellow-dots");

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
		//Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  //      cursor.position = mousePos;
  //      Cursor.visible = false;
    }

    public void StartGame(){
        gameStarted = true;
        UIManager.instance.UpdateScore(-UIManager.instance.GetScore());
        ObjectSpawner.instance.SpawnStarterIngredients(currentStage);
        Cursor.instance.ToggleWand(true);
    }
    public void StopGame(){
        gameStarted = false;
        ObjectSpawner.instance.ClearAllObjects();
    }

    public void ChangeCharacteristicsPool()
    {

    }

    public List<CharacteristicDefinition> GetCharacteristicsForStage(StarterStage stage)
    {
        var initial = new List<CharacteristicDefinition>()
                {
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Frog, Texture = Texture.Stripes, Sprite = stripedSpriteArray[0]
                    }
                };
        switch (stage)
        {
            case StarterStage.Stage2:
                return new List<CharacteristicDefinition>()
                {
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Frog, Texture = Texture.Stripes, Sprite = stripedSpriteArray[0]
                    },
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Frog, Texture = Texture.Plain, Sprite = plainSpriteArray[0]
                    }
                };
            case StarterStage.Stage3:
                return new List<CharacteristicDefinition>()
                {
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Frog, Texture = Texture.Stripes, Sprite = stripedSpriteArray[0]
                    },
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Eye, Texture = Texture.Stripes, Sprite = stripedSpriteArray[4]
                    },
                    new CharacteristicDefinition()
                    {
                        Shape = Shape.Eye, Texture = Texture.Stripes, Sprite = stripedSpriteArray[4]
                    }
                };
            case StarterStage.Stage1:
            default:
                return initial;
        }
    }

    public void IncrementGameStarterStage()
    {
        currentStage++;
    }
}
