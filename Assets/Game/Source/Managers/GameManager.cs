using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<CharacteristicDefinition> Characteristics;
    public Sprite Frog;
    public Sprite Eye;
    public Sprite Chickenfeet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Characteristics = new List<CharacteristicDefinition>()
        {
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Plain, Sprite = Frog },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Spots, Sprite = Frog },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Stripes, Sprite = Frog },

            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Plain, Sprite = Eye },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Spots, Sprite = Eye },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Stripes, Sprite = Eye },

            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Plain, Sprite = Chickenfeet },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Spots, Sprite = Chickenfeet },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Stripes, Sprite = Chickenfeet },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
