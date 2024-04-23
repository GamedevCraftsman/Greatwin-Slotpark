using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlotsManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Button getButton;
	[SerializeField] AudioSource buttonClickSound;
	[Header("Scripts")]
	[SerializeField] WinManager winManagerScript;
	[SerializeField] SlotMachineManager[] slotMachineManagerScripts;
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;
	[Header("Values")]
	[SerializeField] int price;
	[SerializeField] int maxAmountOfUpgrades;
	[SerializeField] int priceMultiplier;
	[SerializeField] int downBorderOfCoins;
	[Header("Texts")]
	[SerializeField] Text buyButtonText;
	[SerializeField] Text multiplierText;

	int currentUpgrade = 0;
	bool canUpgrade;
	UpgradeButton upgradeButtonScript;
	int multiplierSave = 1;
	int nextMultiply = 2;
	void Update()
	{
		GetVerify();
	}

	void GetVerify()
	{
		if (IsMaxLevel())
		{
			PrintName();
		}
		else
		{
			BlockBuyOpportunity();
		}
	}

	bool IsMaxLevel()
	{
		return currentUpgrade < maxAmountOfUpgrades;
	}

	void PrintName()
	{
		multiplierText.text = "x" + multiplierSave.ToString() + " -> x" + nextMultiply + " Coins: " + price;
	}

	void BlockBuyOpportunity()
	{
		getButton.enabled = false;
		multiplierText.text = "MAX";
		buyButtonText.text = "MAX";
	}

	public void SlotsUpgarade()
	{
		buttonClickSound.Play();
		if (IsEnoughtMoney() && !IsAFewMoney())
		{
			playerBalanceManagerScript.ChangeMoney(-price);
			AddEverySlotMultiplier();
			IncreaseAllValues();
		}
		else if (IsAFewMoney())
		{
			winManagerScript.ShowAddMoneyPanel();
		}
	}

	bool IsEnoughtMoney()
	{
		return playerBalanceManagerScript.amountOfCoins - price >= 0;
	}

	void AddEverySlotMultiplier()
	{
		foreach (SlotMachineManager slotMachine in slotMachineManagerScripts)
		{
			slotMachine.multiplier += 1;
		}
	}

	void IncreaseAllValues()
	{
		multiplierSave++;
		nextMultiply++;
		price *= priceMultiplier;
		currentUpgrade++;
	}

	bool IsAFewMoney()
	{
		return playerBalanceManagerScript.amountOfCoins < downBorderOfCoins;
	}
}
