using UnityEngine;
using UnityEngine.UI;

public class GetNewSlotManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] GameObject[] blockPanel;
	[SerializeField] Button buyButton;
	[SerializeField] AudioSource buttonClickSound;
	[Header("Scripts")]
	[SerializeField] SlotMachineManager[] slotMachineManagerScripts;
	[SerializeField] WinManager winManagerScript;
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;
	[Header("Values")]
	[SerializeField] int amountOfSlots;
	[SerializeField] float price;
	[SerializeField] float priceMultiplier;
	[SerializeField] int amountOfNewCells;
	[SerializeField] int downCoinsBorder;
	[Header("Texts")]
	[SerializeField] Text buyButtonText;
	[SerializeField] Text onBuyPanelText;

	[HideInInspector] public int amountOfCells = 5;
	[HideInInspector] public int currentAmountOfSlots = 1;

	bool isMaxBuy = false;
	bool canAdd;
	int amountOfSlot = 0;
	bool isNew;
	int nextAmountOfSlots = 2;
	int newSlots;
	void Update()
	{
		GetVerify();
	}

	void GetVerify()
	{
		if (IsAllSlotsBuy())
		{
			PrintLabel();
		}
		else
		{
			BlockBuyOpportunity();
		}
	}

	bool IsAllSlotsBuy()
	{
		return amountOfSlot != amountOfSlots && !isMaxBuy;
	}

	void PrintLabel()
	{
		onBuyPanelText.text = currentAmountOfSlots.ToString() + "->" + nextAmountOfSlots.ToString() + " Coins: " + price.ToString();
	}

	void BlockBuyOpportunity()
	{
		buyButton.enabled = false;
		isMaxBuy = true;
		buyButtonText.text = "MAX";
		onBuyPanelText.text = "MAX";
	}

	//Function for button.
	public void UnlockNewSlot()
	{
		buttonClickSound.Play();
		if (!IsSlotUnblocked() && IsEnoughMoney() && !IsAFewMoney())
		{
			playerBalanceManagerScript.ChangeMoney(-price);
			OpenSlot();
			IncreaseValues();
		}
		else if(IsAFewMoney())
		{
			winManagerScript.ShowAddMoneyPanel();
		}
	}

	bool IsSlotUnblocked()
	{
		return slotMachineManagerScripts[amountOfSlot].GetComponent<IsSlotBlockedVariable>().isSlotUnlocked;
	}

	bool IsEnoughMoney()
	{
		return playerBalanceManagerScript.amountOfCoins - price >= downCoinsBorder;
	}

	void OpenSlot()
	{
		slotMachineManagerScripts[amountOfSlot].GetComponent<IsSlotBlockedVariable>().isSlotUnlocked = true;
		blockPanel[amountOfSlot].SetActive(false);
	}

	void IncreaseValues()
	{
		price *= priceMultiplier;
		currentAmountOfSlots++;
		nextAmountOfSlots++;
		amountOfSlot++;
		amountOfCells += amountOfNewCells;
	}

	bool IsAFewMoney()
	{
		return playerBalanceManagerScript.amountOfCoins < downCoinsBorder;
	}
}
