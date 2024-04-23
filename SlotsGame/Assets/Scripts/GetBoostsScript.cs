using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GetBoostsScript : MonoBehaviour
{
	[Header("General")]
	[SerializeField] GameObject bottleIcon;
	[SerializeField] GameObject timerPanel;
	[SerializeField] AudioSource buttonClickSound;
	[Header("Scripts")]
	[SerializeField] SlotMachineManager[] slotMachineManagerScripts;
	[SerializeField] WinManager winManagerScript;
	[SerializeField] PlayerBalanceManager playerBalanceManager;
	[Header("Values")]
	[SerializeField] float fadeTime;
	[SerializeField] int price;
	[SerializeField] int boostMultiplier;
	[SerializeField] int downCoinsBorder;
	[Header("Images")]
	[SerializeField] Image fillLine;
	[Header("Buttons")]
	[SerializeField] Button[] blockButtons;
	[SerializeField] Button getButton;
	[Header("Texts")]
	[SerializeField] Text boostTimeText;
	[SerializeField] Text timerText;

	GameObject boost;
	float boostTime = 0;
	int numberOfBoost;
	bool isBoughtBoost = false;
	int defaultMultiplier = 1;
	bool canAddBoost;
	void Start()
	{
		PrintTime();
	}

	void PrintTime()
	{
		boostTimeText.text = boostTime.ToString() + "/60";
	}

	void Update()
	{
		if (isBoughtBoost)
		{
			TimeCounter();
		}
	}

	void TimeCounter()
	{
		if (isTimeEnd())
		{
			CountTime();
		}
		else
		{
			StartCoroutine(HideTimer());
			UnblockButtons();
			SetDefaultValues();
			PrintTime();
		}
	}
	bool isTimeEnd()
	{
		return boostTime > 0;
	}

	void CountTime()
	{
		DisplayTime(boostTime);
		boostTime -= Time.deltaTime;
	}

	IEnumerator HideTimer()
	{
		getButton.enabled = false;
		timerPanel.transform.DOScale(0f, fadeTime).SetEase(Ease.InExpo);
		yield return new WaitForSeconds(fadeTime);
		bottleIcon.SetActive(false);
		getButton.enabled = true;
	}

	void UnblockButtons()
	{
		foreach (Button button in blockButtons)
		{
			button.interactable = true;
		}
	}

	void DisplayTime(float time)
	{
		time = Mathf.FloorToInt(time % 60);
		boostTimeText.text = time.ToString() + "/60";
		timerText.text = time.ToString();
		fillLine.fillAmount = time * 0.017f;
	}

	void SetDefaultValues()
	{
		isBoughtBoost = false;
		SetDefaultMultiplier();
		boostTime = 0;
	}

	void SetDefaultMultiplier()
	{
		foreach (SlotMachineManager slotMachine in slotMachineManagerScripts)
		{
			slotMachine.boostMultiplier = defaultMultiplier;
		}
	}

	//Function for button. 
	public void BuyBoost()
	{
		buttonClickSound.Play();
		if (isEnoughMoney() && !IsAFewMoney() && boostTime + 10 <= 60)
		{
			ShowTimer();
			BlockButtons();
			playerBalanceManager.ChangeMoney(-price);
			boostTime += 10;
			IncreaseValues();
		}
		else if(IsAFewMoney())
		{
			winManagerScript.ShowAddMoneyPanel();
		}
	}

	bool isEnoughMoney()
	{
		return (playerBalanceManager.amountOfCoins - price >= downCoinsBorder);
	}

	void ShowTimer()
	{
		bottleIcon.SetActive(true);
		timerPanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void BlockButtons()
	{
		foreach (Button button in blockButtons)
		{
			button.interactable = false;
		}
	}

	void IncreaseValues()
	{
		isBoughtBoost = true;
		IncreaseMultiplier();
	}

	bool IsAFewMoney()
	{
		return playerBalanceManager.amountOfCoins < downCoinsBorder;
	}
	void IncreaseMultiplier()
	{
		foreach (SlotMachineManager slotMachine in slotMachineManagerScripts)
		{
			slotMachine.boostMultiplier = boostMultiplier;
		}
	}
}
