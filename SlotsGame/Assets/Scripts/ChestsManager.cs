using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestsManager : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] int prizeFrom;
	[SerializeField] int prizeTo;
	[SerializeField] int addend;
	[SerializeField] float fadeTime;
	[SerializeField] float endFadeTime;
	[Header("Scripts")]
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;
	[Header("Gameobjects")]
	[SerializeField] GameObject backPanel;
	[SerializeField] GameObject chestPanel;
	[SerializeField] GameObject[] chests;
	[SerializeField] GameObject[] openedChests;
	[SerializeField] GameObject[] winCoinsTexts;
	[Header("AudioSources")]
	[SerializeField] AudioSource openChestSound;
	[SerializeField] AudioSource coinsFallSound;
	
	int prize;
	//button function.
	public void OpenChest(int chestNumber)
	{
		CountWinPrize(chestNumber);
		StartCoroutine(ShowEffects(chestNumber));
		IncreaseValues();
	}

	void CountWinPrize(int chestNumber)
	{
		prize = Random.Range(prizeFrom, prizeTo);
		playerBalanceManagerScript.ChangeMoney(prize);
		winCoinsTexts[chestNumber].GetComponentInChildren<Text>().text = prize.ToString();
	}

	IEnumerator ShowEffects(int chestNumber)
	{
		BlockButtons();
		HideCloseChest(chestNumber);
		yield return new WaitForSeconds(fadeTime);
		openChestSound.Play();
		chests[chestNumber].SetActive(false);
		ShowOpenChest(chestNumber);
		yield return new WaitForSeconds(fadeTime);
		coinsFallSound.Play();
		ShowWinCoins(chestNumber);
		yield return new WaitForSeconds(endFadeTime);
		chestPanel.transform.DOScale(0f, endFadeTime).SetEase(Ease.InExpo);
		yield return new WaitForSeconds(endFadeTime);
		backPanel.SetActive(false);
		SetDefault(chestNumber);
	}

	void BlockButtons()
	{
		foreach (GameObject chest in chests)
		{
			chest.GetComponent<Button>().interactable = false;
		}
	}

	void HideCloseChest(int chestNumber)
	{
		chests[chestNumber].transform.DOScale(0.5f, fadeTime).SetEase(Ease.InExpo);
	}

	void ShowOpenChest(int chestNumber)
	{
		openedChests[chestNumber].transform.localScale = new Vector3(0.5f, 0.5f, 0);
		openedChests[chestNumber].SetActive(true);
		openedChests[chestNumber].transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void ShowWinCoins(int chestNumber)
	{
		winCoinsTexts[chestNumber].transform.localScale = Vector3.zero;
		winCoinsTexts[chestNumber].SetActive(true);
		winCoinsTexts[chestNumber].transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void IncreaseValues()
	{
		prizeFrom += addend;
		prizeTo += addend;
	}

	void SetDefault(int chestNumber)
	{
		SetDefaultStates(chestNumber);
		UnblockButtons();
		SetDefaultValues();
	}

	void SetDefaultStates(int chestNumber)
	{
		openedChests[chestNumber].SetActive(false);
		chests[chestNumber].SetActive(true);
		chests[chestNumber].transform.localScale = Vector3.one;
		winCoinsTexts[chestNumber].SetActive(false);
	}

	void UnblockButtons()
	{
		foreach (GameObject chest in chests)
		{
			chest.GetComponent<Button>().interactable = true;
		}
	}

	void SetDefaultValues()
	{
		prize = 0;
	}
}
