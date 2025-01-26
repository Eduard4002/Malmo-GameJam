using UnityEngine;

public class Digit : MonoBehaviour
{
	[SerializeField] 
	private Sprite[] digits;
	private SpriteRenderer sr;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	public void Set(int digit)
	{
		sr.sprite = digits[digit];
	}
}
