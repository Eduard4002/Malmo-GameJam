using UnityEngine;
public class Number : MonoBehaviour
{
	private Digit[] digits;
	private float curNumber;
	private float targetNumber;

	private void Awake()
	{
		digits = GetComponentsInChildren<Digit>();
	}

	public void UpdateNumber(int number)
	{
		targetNumber = number;
	}

	private void Update()
	{
		curNumber = Mathf.Lerp(curNumber, targetNumber, Time.deltaTime * 5);
		Set(Mathf.RoundToInt(curNumber));
	}

	private void Set(int number)
	{
		number = Mathf.Clamp(number, 0, 9999999);
		string n = number.ToString("D7");

		for (int i = 0; i < n.Length; i++)
		{
			string digit = n[i].ToString();
			int num = int.Parse(digit);
			digits[i].Set(num);
		}
	}
}
