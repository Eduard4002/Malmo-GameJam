using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : Singleton<ObjectSelection>
{
	[SerializeField, Tooltip("How long can the user select (seconds)")] float timerAmount = 10f;
	private bool timerStarted;
	private float timeLeft;

	public void CheckSelection(List<Object> ingredients)
	{
		bool succeded = CheckSuccession(ingredients);
		Debug.Log("Correct selection: " + succeded);
		int numberOfBubbles = ingredients.Count;

		if (succeded)
		{
			//Calculate the score based on the items selected
			//Destroy the items only if we succeded
			for (int i = 0; i < numberOfBubbles; i++)
			{
				GridSystem.instance.RemoveSlot(ingredients[i].SpawnIndex);
				ObjectSpawner.instance.RemoveObjectSpawned(ingredients[i].SpawnIndex);
				UIManager.instance.UpdateScore(numberOfBubbles * 2);
				ingredients[i].DeleteSelf();
			}
			ObjectSpawner.instance.SpawnNewIngredients(numberOfBubbles);
		}else{
            UIManager.instance.UpdateScore(-(numberOfBubbles * 2));

        }
	}

	private bool CheckSuccession(List<Object> items)
	{
		if (items == null || items.Count == 0)
			return false;

		// Require atleast 2 objects
		if (items.Count < 2)
			return false;

		//Check if the user has picked the correct or wrong ingredients
		Object check = items[0];
		for (int i = 0; i < items.Count; i++)
		{
			var item = items[i];
			var sameShape = item.Characteristics.Shape == check.Characteristics.Shape;
			var sameTexture = item.Characteristics.Texture == check.Characteristics.Texture;

			if (!sameShape && !sameTexture)
				return false;
		}
		return true;
	}
}
