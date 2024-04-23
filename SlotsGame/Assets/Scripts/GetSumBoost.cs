using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GetSumBoost : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Text toxtOnButton;
	[SerializeField] Text textOfspinsLeft;
	[SerializeField] Button[] blockButtons;
	[SerializeField] GameObject winCoinsText;
	[SerializeField] AudioSource buttonClick;
	[Header("Values")]
	[SerializeField] int lowCoinsBorder;
	[SerializeField] int fadeTime;
	[SerializeField] int multiplier;
	[SerializeField] int boostNum;
	[SerializeField] int price;
	[SerializeField] int leftSpins;
	[Header("Scripts")]
	[SerializeField] WinManager winManagerScript;
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;

	[HideInInspector] public bool canCount = false;

	int saveSpinsLeft;
	double fullPrize = 0;
	void Start()
	{
		saveSpinsLeft = leftSpins;
	}

	//button function.
	public void AllowCountFullPrize()
	{
		buttonClick.Play();
		if (IsEnoghtMoney() && !IsAFewMoney())
		{
			toxtOnButton.text = "GOT";
			playerBalanceManagerScript.ChangeMoney(-price);
			winManagerScript.numberOfBoost = boostNum;
			BlockeButtons();
			textOfspinsLeft.text = leftSpins.ToString() + "/3";
			canCount = true;
		}
		else if (IsAFewMoney())
		{
			winManagerScript.ShowAddMoneyPanel();
		}
	}

	bool IsAFewMoney()
	{
		return playerBalanceManagerScript.amountOfCoins < lowCoinsBorder;
	}

	bool IsEnoghtMoney()
	{
		return playerBalanceManagerScript.amountOfCoins - price >= lowCoinsBorder;
	}

	void BlockeButtons()
	{
		gameObject.GetComponent<Button>().enabled = false;
		foreach (Button button in blockButtons)
		{
			button.interactable = false;
		}
	}

	[HideInInspector]
	public void CountThreePrizes(double currentPrize)
	{
		if (isSpins())
		{
			CountPrize(currentPrize);
		}

		if (!isSpins())
		{
			StartCoroutine(ShowWinCoins());
			UnlockButtons();
		}
	}

	bool isSpins()
	{
		return leftSpins > 0;
	}

	void CountPrize(double currentPrize)
	{
		fullPrize += currentPrize;
		leftSpins--;
		textOfspinsLeft.text = leftSpins.ToString() + "/3";
	}

	void SetDefaulValues()
	{
		toxtOnButton.text = "GET";
		textOfspinsLeft.text = "0/3";
		leftSpins = saveSpinsLeft;
		canCount = false;
		fullPrize = 0;
	}
	void UnlockButtons()
	{
		gameObject.GetComponent<Button>().enabled = true;
		foreach (Button button in blockButtons)
		{
			button.interactable = true;
		}
	}
	IEnumerator ShowWinCoins()
	{
		//show win coins.
		winCoinsText.transform.localScale = Vector3.zero;
		winCoinsText.GetComponent<Text>().text = "+" + fullPrize * multiplier + " bonus";
		winCoinsText.SetActive(true);
		winCoinsText.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
		yield return new WaitForSeconds(fadeTime);
		playerBalanceManagerScript.ChangeMoney(fullPrize * multiplier);
		yield return new WaitForSeconds(fadeTime);
		//hide win coins.
		winCoinsText.transform.DOScale(0f, fadeTime).SetEase(Ease.InExpo);
		yield return new WaitForSeconds(fadeTime);
		SetDefaulValues();
		winCoinsText.SetActive(false);
	}
}
