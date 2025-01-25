using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<CharacteristicDefinition> Characteristics;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Characteristics = new List<CharacteristicDefinition>()
        {
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Plain },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Spots },
            new CharacteristicDefinition{Shape = Shape.Frog, Texture = Texture.Stripes },

            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Plain },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Spots },
            new CharacteristicDefinition{Shape = Shape.Eye, Texture = Texture.Stripes },

            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Plain },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Spots },
            new CharacteristicDefinition{Shape = Shape.Chickenfeet, Texture = Texture.Stripes },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
