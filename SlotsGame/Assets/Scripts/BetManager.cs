using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] double[] bets;
	
	[SerializeField] float endAnimTime;
	[SerializeField] float fadeTime;
	[SerializeField] int diapazoneOfNumbers;
	[Header("Gameobjects")]
	[SerializeField] Text betText;
	[SerializeField] GameObject backText;
	[SerializeField] AudioSource buttonClick;

	[HideInInspector] public double bet;

	bool isBid;
	int num;
	int betNumber = 0;
	float endSizeOfObject = 1;
	void Start()
	{
		betText.text = bets[betNumber].ToString();
		bet = bets[betNumber];
	}

	public void PlusBet()
	{
		buttonClick.Play();
		if (IsNextObject())
		{
			TakeNextBet();
		}
	}

	bool IsNextObject()
	{
		return betNumber + diapazoneOfNumbers < bets.Length;
	}

	void TakeNextBet()
	{
		betNumber++;
		bet = bets[betNumber];
		StartCoroutine(TextChanger());
		betText.text = bets[betNumber].ToString();
	}

	public void MinusBet()
	{
		buttonClick.Play();
		if (IsPreviousObject())
		{
			TakePreviousBet();
		}
	}

	bool IsPreviousObject()
	{
		return betNumber - diapazoneOfNumbers >= 0;
	}

	void TakePreviousBet()
	{
		betNumber--;
		bet = bets[betNumber];
		StartCoroutine(TextChanger());
		betText.text = bets[betNumber].ToString();
	}

	IEnumerator TextChanger()
	{
		AppearNumber();
		yield return new WaitForSeconds(endAnimTime);
	}

	void AppearNumber()
	{
		backText.transform.localScale = Vector3.zero;
		backText.transform.DOScale(endSizeOfObject, fadeTime).SetEase(Ease.OutExpo);
	}
}
