using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum HandValue
{
	Rock,
	Paper,
	Scissors
}
public class RPSGameManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Combinations[] combinations;
	[SerializeField] Slider bidSlider;
	[Header("Scripts")]
	[SerializeField] PlayerBalanceManager moneyManagerScript;
	[SerializeField] WinManager spinScript;
	[Header("Values")]
	[SerializeField] int startFont;
	[SerializeField] int changeFont;
	[SerializeField] int prizeMultiplier;
	[SerializeField] float lowCoinsBorder;
	[SerializeField] float waitTime;
	[SerializeField] float gameTime;
	[SerializeField] float stepAmount;
	[SerializeField] float numberOfSteps;
	[SerializeField] float fadeTime;
	[Header("Buttons")]
	[SerializeField] Button[] playerHandButtons;
	[SerializeField] Button gameButton;
	[SerializeField] Button closeButton;
	[SerializeField] Button playButton;
	[Header("Text")]
	[SerializeField] Text bidText;
	[SerializeField] Text gameTimeText;
	[SerializeField] Text prizeText;
	[Header("GameObjects")]
	[SerializeField] GameObject[] opponentHands;
	[SerializeField] GameObject defaultDialogPanel;
	[SerializeField] GameObject winDialogPanel;
	[SerializeField] GameObject loseDialogPanel;
	[SerializeField] GameObject backDarkPanel;
	[SerializeField] GameObject rpsGamePanel;
	[Header("AudioSources")]
	[SerializeField] AudioSource buttonClick;
	[SerializeField] AudioSource winSound;
	[SerializeField] AudioSource loseSound;
 
	int prize;
	float saveGameTime;
	bool isWin = false;
	HandValue playerHandName;
	HandValue opponentHandName;
	int opponentHandNumber;
	void Start()
	{
		bidText.text = bidSlider.value.ToString();
		numberOfSteps = (int)bidSlider.maxValue / stepAmount;
		saveGameTime = gameTime;
	}

	void Update()
	{
		CanPlayGame();
	}

	void CanPlayGame()
	{
		if (IsTimeEnd())
		{
			UnlockGameButton();
		}
		else
		{
			CountTime();
		}
	}

	bool IsTimeEnd()
	{
		return gameTime <= 0;
	}

	void CountTime()
	{
		DisplayTime(gameTime);
		gameTime -= Time.deltaTime;
		gameButton.interactable = false;
	}

	void UnlockGameButton()
	{
		gameButton.interactable = true;
		gameTimeText.fontSize = changeFont;
		gameTimeText.text = "Let’s play!";
	}

	void DisplayTime(float time)
	{
		time = Mathf.FloorToInt(time % 60);
		gameTimeText.text = time.ToString();
	}

	//Open button.
	public void OpenGamePanel()
	{
		buttonClick.Play();
		rpsGamePanel.transform.localScale = Vector3.zero;
		BackDarkPanelOpen(true);
		RPSGamePanelOpen(true);
		ShowPanel();
	}

	void ShowPanel()
	{
		rpsGamePanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void BackDarkPanelOpen(bool panelStatus)
	{
		backDarkPanel.SetActive(panelStatus);
	}

	void RPSGamePanelOpen(bool panelStatus)
	{
		rpsGamePanel.SetActive(true);

	}

	// Close button.
	public void CloseGamePanel()
	{
		buttonClick.Play();
		StartCoroutine(ClosePanel());
	}

	IEnumerator ClosePanel()
	{
		closeButton.enabled = false;
		HidePanel();
		yield return new WaitForSeconds(fadeTime);
		BackDarkPanelOpen(false);
		RPSGamePanelOpen(false);
		closeButton.enabled = true;
	}

	void HidePanel()
	{
		rpsGamePanel.transform.DOScale(0f, fadeTime).SetEase(Ease.InExpo);
	}

	public void SliderBid()
	{
		float range = (bidSlider.value / bidSlider.maxValue) * numberOfSteps;
		int ceil = Mathf.CeilToInt(range);
		bidSlider.value = ceil * stepAmount;
		bidText.text = bidSlider.value.ToString();
	}

	//Play button.
	public void PlayButton()
	{
		buttonClick.Play();
		if (IsEnoughMoney() && !IsAFewMoney())
		{
			bidSlider.interactable = false;
			moneyManagerScript.ChangeMoney(-bidSlider.value);
			UnlockPlayerHandButtons();
			BlockOtherButtons();
		}
		else if (IsAFewMoney())
		{
			spinScript.ShowAddMoneyPanel();
		}
	}

	bool IsAFewMoney()
	{
		return moneyManagerScript.amountOfCoins < lowCoinsBorder;
	}

	bool IsEnoughMoney()
	{
		return moneyManagerScript.amountOfCoins - bidSlider.value >= lowCoinsBorder;
	}

	void UnlockPlayerHandButtons()
	{
		foreach (Button button in playerHandButtons)
		{
			button.interactable = true;
		}
	}

	void BlockOtherButtons()
	{
		closeButton.interactable = false;
		playButton.interactable = false;
	}

	//Player hand button.
	public void CheckPlayResults(int PlayerHandNum)
	{
		buttonClick.Play();
		StartCoroutine(CheckChoice(PlayerHandNum));
	}

	IEnumerator CheckChoice(int playerHandNum)
	{
		CheckPlayerChoice(playerHandNum);
		CheckWinner();
		BlockPlayerHand();
		yield return new WaitForSeconds(waitTime);
		StartCoroutine(ClosePanel());
		yield return new WaitForSeconds(fadeTime);
		SetDefaultValues();
	}

	void CheckPlayerChoice(int playerHandNum)
	{
		switch (playerHandNum)
		{
			case 0:
				playerHandName = HandValue.Rock;
				break;
			case 1:
				playerHandName = HandValue.Paper;
				break;
			case 2:
				playerHandName = HandValue.Scissors;
				break;
		}
	}

	void CheckWinner()
	{
		for (int i = 0; i < combinations.Length; i++)
		{
			if (IsPlayerWin(i))
			{
				winSound.Play();
				CountPrize();
			}
		}

		if (!isWin)
		{
			  loseSound.Play();
			StartCoroutine(ShowDialogPanel(loseDialogPanel));
		}
	}

	bool IsPlayerWin(int i)
	{
		return playerHandName == combinations[i].playerHand && OpponentChoice() == combinations[i].opponentHand;
	}

	void CountPrize()
	{
		prize = (int)bidSlider.value * prizeMultiplier;
		prizeText.text = prize + " coins.";
		StartCoroutine(ShowDialogPanel(winDialogPanel));
		isWin = true;
		moneyManagerScript.ChangeMoney(prize);
	}

	IEnumerator ShowDialogPanel(GameObject dialogPanel)
	{
		yield return new WaitForSeconds(fadeTime);
		defaultDialogPanel.SetActive(false);
		dialogPanel.transform.localScale = Vector3.zero;
		dialogPanel.SetActive(true);
		dialogPanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
		yield return new WaitForSeconds(waitTime);
		dialogPanel.SetActive(false);
		defaultDialogPanel.SetActive(true);
	}

	HandValue OpponentChoice()
	{
		opponentHandNumber = Random.Range(0, 3);
		ShowOpponentChoice();
		CheckOpponentChoice();
		return opponentHandName;
	}

	void ShowOpponentChoice()
	{
		opponentHands[opponentHandNumber].transform.localScale = Vector3.zero;
		opponentHands[opponentHandNumber].SetActive(true);
		opponentHands[opponentHandNumber].transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void CheckOpponentChoice()
	{
		switch (opponentHandNumber)
		{
			case 0:
				opponentHandName = HandValue.Rock;
				break;
			case 1:
				opponentHandName = HandValue.Paper;
				break;
			case 2:
				opponentHandName = HandValue.Scissors;
				break;
		}
	}

	void BlockPlayerHand()
	{
		foreach (Button button in playerHandButtons)
		{
			button.interactable = false;
		}
	}

	void SetDefaultValues()
	{
		bidSlider.interactable = true;
		gameTimeText.fontSize = startFont;
		gameTime = saveGameTime;
		prize = 0;
		isWin = false;
		bidSlider.value = bidSlider.minValue;
		opponentHands[opponentHandNumber].SetActive(false);
		closeButton.interactable = true;
		playButton.interactable = true;
	}
}

[System.Serializable]
class Combinations
{
	public HandValue playerHand;
	public HandValue opponentHand;
}
