using System.Collections.Generic;
using UnityEngine;
public class Number : MonoBehaviour
{
	private Digit[] digits;
	private int curNumber;
	private int targetNumber;

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
		int number = Mathf.CeilToInt(Mathf.Lerp(curNumber, targetNumber, Time.deltaTime * 5));
		if (number > curNumber)
			Set(number);
	}

	private void Set(int number)
	{
		curNumber = number;
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
