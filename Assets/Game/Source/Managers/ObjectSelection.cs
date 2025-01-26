using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class ObjectSelection : Singleton<ObjectSelection>
{
	public EventReference successSound;
	public EventReference failureSound;

	public EventReference witchSuccessSound;
	public EventReference witchFailureSound;

	public void CheckSelection(List<Object> ingredients)
	{
		bool succeded = CheckSuccession(ingredients);
		Debug.Log("Correct selection: " + succeded);
		int numberOfBubbles = ingredients.Count;

		if (succeded)
		{
			AudioManager.Instance.PlayOneShot(successSound);
			AudioManager.Instance.PlayOneShotDelayed(witchSuccessSound, 1f);
			//Calculate the score based on the items selected
			//Destroy the items only if we succeded
			for (int i = 0; i < numberOfBubbles; i++)
			{
				GridSystem.instance.RemoveSlot(ingredients[i].SpawnIndex);
				ObjectSpawner.instance.RemoveObjectSpawned(ingredients[i].SpawnIndex);
				UIManager.instance.UpdateScore(Mathf.CeilToInt(Mathf.Pow(numberOfBubbles, 1.75f) * 10));
				ingredients[i].DeleteSelf();
			}

			if((int)GameManager.instance.currentStage >= 3)
			{
                ObjectSpawner.instance.SpawnNewIngredients();
            }
			else
			{
                ObjectSpawner.instance.SpawnStarterIngredients(GameManager.instance.currentStage);
            }

		} else {
			for (int i = 0; i < numberOfBubbles; i++)
			{
				ingredients[i].Fail();
			}
			AudioManager.Instance.PlayOneShot(failureSound);
            AudioManager.Instance.PlayOneShotDelayed(witchFailureSound, 1f);
            UIManager.instance.UpdateScore(-50);
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
