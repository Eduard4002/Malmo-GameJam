using UnityEngine;

public class WandPillow : MonoBehaviour
{
	public Sprite wand, noWand;
	private SpriteRenderer sr;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	public void ToggleWand(bool wandOn)
	{
		sr.sprite = wandOn ? wand : noWand;
	}
}
