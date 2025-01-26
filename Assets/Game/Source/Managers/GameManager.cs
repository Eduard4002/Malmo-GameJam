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

    public Stages currentStage;

    Sprite[] plainSpriteArray;
    Sprite[] zigzagSpriteArray;
    Sprite[] stripedSpriteArray;
    Sprite[] dotsSpriteArray;

    List<CharacteristicDefinition> stage4Definitions;
    List<CharacteristicDefinition> stage5Definitions;
    List<CharacteristicDefinition> stage6Definitions;
    List<CharacteristicDefinition> stage7Definitions;
    List<CharacteristicDefinition> stage8Definitions;
    List<CharacteristicDefinition> stage9Definitions;


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
            GetSpriteForCharacteristic(Shape.Frog, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Frog, Texture.Stripes),

            GetSpriteForCharacteristic(Shape.Eye, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Eye, Texture.Stripes),

            GetSpriteForCharacteristic(Shape.Chickenfeet, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Chickenfeet, Texture.Stripes),
        };

        stage5Definitions = new List<CharacteristicDefinition>()
        {
            GetSpriteForCharacteristic(Shape.Frog, Texture.Spots),
            GetSpriteForCharacteristic(Shape.Eye, Texture.Spots),
            GetSpriteForCharacteristic(Shape.Chickenfeet, Texture.Spots),
        };

        stage6Definitions = new List<CharacteristicDefinition>()
        {
            GetSpriteForCharacteristic(Shape.Skull, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Skull, Texture.Stripes),
            GetSpriteForCharacteristic(Shape.Skull, Texture.Spots),
        };

        stage7Definitions = new List<CharacteristicDefinition>()
        {
            GetSpriteForCharacteristic(Shape.Frog, Texture.ZigZag),
            GetSpriteForCharacteristic(Shape.Eye, Texture.ZigZag),
            GetSpriteForCharacteristic(Shape.Chickenfeet, Texture.ZigZag),
        };

        stage8Definitions = new List<CharacteristicDefinition>()
        {
            GetSpriteForCharacteristic(Shape.Fish, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Fish, Texture.Stripes),
            GetSpriteForCharacteristic(Shape.Fish, Texture.Spots),
            GetSpriteForCharacteristic(Shape.Fish, Texture.ZigZag),
        };

        stage9Definitions = new List<CharacteristicDefinition>()
        {
            GetSpriteForCharacteristic(Shape.Mushroom, Texture.Plain),
            GetSpriteForCharacteristic(Shape.Mushroom, Texture.Stripes),
            GetSpriteForCharacteristic(Shape.Mushroom, Texture.Spots),
            GetSpriteForCharacteristic(Shape.Mushroom, Texture.ZigZag),
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

    public List<CharacteristicDefinition> GetCharacteristicsForStage(Stages stage)
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
            case Stages.Stage2:
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
            case Stages.Stage3:
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
            case Stages.Stage1:
            default:
                return initial;
        }
    }

    public void IncrementGameStarterStage()
    {
        currentStage++;
    }

    public void CheckScoreLevel()
    {
        switch (UIManager.instance.GetScore())
        {
            case > 10000:

                if ((int)currentStage >= (int)Stages.Stage9)
                    break;
				DrawManager.Instance.SetDrawTime(2);
				currentStage = Stages.Stage9;
                Characteristics.AddRange(stage9Definitions);
                ObjectSpawner.instance.ChangeMaxAmount(15);
                break;
            case > 8000:

                if ((int)currentStage >= (int)Stages.Stage8)
                    break;
				DrawManager.Instance.SetDrawTime(3);
				currentStage = Stages.Stage8;
                Characteristics.AddRange(stage8Definitions);
                ObjectSpawner.instance.ChangeMaxAmount(13);
                break;
            case > 6000:

                if ((int)currentStage >= (int)Stages.Stage7)
                    break;

				DrawManager.Instance.SetDrawTime(4);
				currentStage = Stages.Stage7;
                Characteristics.AddRange(stage7Definitions);
                ObjectSpawner.instance.ChangeMaxAmount(11);
                break;
            case > 4000:

                if ((int)currentStage >= (int)Stages.Stage6)
                    break;
				DrawManager.Instance.SetDrawTime(6);
				currentStage = Stages.Stage6;
                Characteristics.AddRange(stage6Definitions);
                ObjectSpawner.instance.ChangeMaxAmount(9);
                break;
            case > 2000:
                if ((int)currentStage >= (int)Stages.Stage5)
                    break;
                DrawManager.Instance.SetDrawTime(8);
                currentStage = Stages.Stage5;
                Characteristics.AddRange(stage5Definitions);
                ObjectSpawner.instance.ChangeMaxAmount(7);
                break;
        }
    }

    private CharacteristicDefinition GetSpriteForCharacteristic(Shape shape, Texture texture)
    {
        var characteristic = new CharacteristicDefinition()
        {
            Shape = shape,
            Texture = texture,
            Sprite = stripedSpriteArray[0]
        };

        switch (texture)
        {
            case Texture.Plain:
                characteristic.Sprite = plainSpriteArray[(int)shape];
                break;
            case Texture.Spots:
                characteristic.Sprite = dotsSpriteArray[(int)shape];
                break;
            case Texture.ZigZag:
                characteristic.Sprite = zigzagSpriteArray[(int)shape];
                break;
            case Texture.Stripes:
            default:
                characteristic.Sprite = stripedSpriteArray[(int)shape];
                break;
        }
        return characteristic;
    }
}

public enum Stages
{
    Stage1,
    Stage2,
    Stage3, //last starter stage
    Stage4,
    Stage5,
    Stage6,
    Stage7,
    Stage8,
    Stage9
}