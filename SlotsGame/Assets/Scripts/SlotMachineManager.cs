using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotMachineManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Button spinButton;
	[SerializeField] Animator addMoneyAnim;
	[SerializeField] AudioSource cellRotationSound;
	[Header("Scripts")]
	[SerializeField] CellRotationScript[] cellRotationScripts;
	[SerializeField] PlayerBalanceManager playerBalanceManagerScripts;
	[SerializeField] WinManager winManagerScript;
	[SerializeField] FillXPManager fillXpManagerScript;
	[Header("Values")]
	[SerializeField] int stoppedSlots = 5;
	[SerializeField] int timeToShowingWinCoins;
	[SerializeField] int amountOfSlots;
	[SerializeField] int amountOfSameIcons;
	[SerializeField] float fadeTime;
	public int addendMegaWin;
	public int addendBigWin;
	[Header("Gameobjects")]
	[SerializeField] GameObject amountWinCoin;
	[SerializeField] GameObject addMoneyDarkPanel;

	[HideInInspector] public bool isStop = false;
	[HideInInspector] public bool canXpAdd = false;
	[HideInInspector] public int multiplier = 1;
	[HideInInspector] public int boostMultiplier = 1;
	[HideInInspector] public double prize;

	List<int> indexMaxIconCount = new List<int>();
	List<int> amountOfIcons = new List<int>();

	string[] iconsNames = { "Cherry", "BlueHeart", "Lance", "Strawberry", "Bell", "Crown" };
	int xpDivider = 1000;
	int saveStoppedSlots = 5;
	int amount;
	void Start()
	{
		SetXpDivider();
	}

	void SetXpDivider()
	{
		xpDivider = fillXpManagerScript.xpDivider;
	}

	public void WaitResults()
	{
		stoppedSlots--;
		if (IsSlotsStopped())
		{
			stoppedSlots = saveStoppedSlots;
			CheckResults();
		}
	}

	bool IsSlotsStopped()
	{
		return stoppedSlots <= 0;
	}

	void CheckResults()
	{
		prize = winManagerScript.playerBet;
		CountSameIcons();
		FindTheBiggestNumber();
		GiveAPrize();
		playerBalanceManagerScripts.ChangeMoney(prize);


		amountOfIcons.Clear();
		indexMaxIconCount.Clear();
		cellRotationSound.Stop();
		isStop = true;
	}

	void CountSameIcons()
	{
		for (int i = 0; i <= amountOfSlots; i++)
		{
			amount = 0;
			for (int j = 0; j < cellRotationScripts.Length; j++)
			{
				if (IsIconSame(i, j))
				{
					amount++;
				}
				if (IsSlotDouble(j))
				{
					if (IsIconSameUpperSlot(i, j))
					{
						amount++;
					}
				}
			}
			amountOfIcons.Add(amount);
		}
	}
	bool IsIconSame(int i, int j)
	{
		return iconsNames[i].ToString() == cellRotationScripts[j].gameObject.GetComponent<CellRotationScript>().stoppedSlot[0].ToString();
	}

	bool IsSlotDouble(int j)
	{
		return cellRotationScripts[j].gameObject.GetComponent<CellRotationScript>().isDoubleSlot;
	}

	bool IsIconSameUpperSlot(int i, int j)
	{
		return iconsNames[i].ToString() == cellRotationScripts[j].gameObject.GetComponent<CellRotationScript>().stoppedSlot[1].ToString();
	}
	void FindTheBiggestNumber()
	{
		for (int i = 0; i < amountOfIcons.Count; i++)
		{
			if (IsThisNumberTheBiggest(i))
			{
				indexMaxIconCount.Add(i);
			}
		}
	}

	bool IsThisNumberTheBiggest(int i)
	{
		return amountOfIcons[i] == FindMaxElement(amountOfIcons);
	}

	void GiveAPrize()
	{
		for (int i = 0; i < indexMaxIconCount.Count; i++)
		{
			if (IsEnoughSameIcons(i))
			{
				CountPrize(i);
			}
			else
			{
				Lose();
			}
		}
	}

	bool IsEnoughSameIcons(int i)
	{
		return amountOfIcons[indexMaxIconCount[i]] >= amountOfSameIcons;
	}

	void CountPrize(int i)
	{
		prize *= amountOfIcons[indexMaxIconCount[i]] * multiplier * boostMultiplier;
		fillXpManagerScript.xpCount += prize / xpDivider;
		canXpAdd = true;
		StartCoroutine(ShowHowManyWin());
	}

	void Lose()
	{
		prize = 0;
	}

	//Show how many coins player win.
	IEnumerator ShowHowManyWin()
	{
		amountWinCoin.transform.localScale = Vector3.zero;
		PrintHowManyWin();
		IsNubersShow(true);
		AppearNumber();
		yield return new WaitForSeconds(timeToShowingWinCoins);
		HideNumber();
		yield return new WaitForSeconds(fadeTime);
		IsNubersShow(false);
	}

	void PrintHowManyWin()
	{
		amountWinCoin.GetComponentInChildren<Text>().text = "+ " + prize;
	}

	void IsNubersShow(bool showHide)
	{
		amountWinCoin.SetActive(showHide);
	}

	void AppearNumber()
	{
		amountWinCoin.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void HideNumber()
	{
		amountWinCoin.transform.DOScale(0f, fadeTime).SetEase(Ease.InExpo);
	}

	//This function is searching the biggest number of list.
	static T FindMaxElement<T>(List<T> list) where T : IComparable<T>
	{
		T maxElement = list[0];
		for (int i = 1; i < list.Count; i++)
		{
			if (list[i].CompareTo(maxElement) > 0)
			{
				maxElement = list[i];
			}
		}
		return maxElement;
	}
}
