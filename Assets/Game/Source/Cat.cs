using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
	[SerializeField] private Sprite normal;
	[SerializeField] private Sprite blink;
	private SpriteRenderer sr;

	private float timer;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer = Random.Range(10, 15);
			StartCoroutine(Blink());
		}
	}

	IEnumerator Blink()
	{
		sr.sprite = blink;
		yield return new WaitForSeconds(0.1f);
		sr.sprite = normal;
	}
}
