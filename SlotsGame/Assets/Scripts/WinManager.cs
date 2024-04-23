using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Image chargeFill;
	[SerializeField] GameObject addMoneyDarkPanel;
	[SerializeField] Text amountOfAllSpinsText;
	[SerializeField] Camera bakeCamera;
	[SerializeField] GameObject winParticles;
	[Header("Values")]
	[SerializeField] int multiplier;
	[SerializeField] float fadeTime;
	[SerializeField] float timeToDestroyParticles;
	[SerializeField] float timeAfterShowingText;
	[SerializeField] float addMoneyIfFew;
	[SerializeField] double wheelDivider;
	[Header("Scripts")]
	[SerializeField] GetSumBoost[] getSumBoostScripts;
	[SerializeField] GetNewSlotManager getNewSloatManagerScript;
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;
	[SerializeField] BetManager bidManagerScript;
	[SerializeField] GameObject[] slotMachineManagerScripts;
	[SerializeField] CellRotationScript[] cellRotationScripts;
	[Header("Animators")]
	[SerializeField] Animator addMoneyPanelAnim;
	[Header("Buttons")]
	[SerializeField] Button spinButton;
	[SerializeField] Button upgradeButton;
	[Header("Mega Win")]
	[SerializeField] GameObject megawinPointOfSpawnParticles;
	[SerializeField] GameObject megaWin;
	[SerializeField] GameObject megaWinDarkBg;
	[SerializeField] Text prizeTextMegaWin;
	[SerializeField] AudioSource megaWinSound;
	[Header("Big win")]
	[SerializeField] GameObject bigwinPointOfSpawnParticles;
	[SerializeField] GameObject bigWin;
	[SerializeField] GameObject bigWinDarkBg;
	[SerializeField] Text prizeTextBigWin;
	[SerializeField] AudioSource bigWinSound;
	[Header("General AudioSources")]
	[SerializeField] AudioSource cellRotationSound;
	[SerializeField] AudioSource collectMoneyShowSound;
	[SerializeField] AudioSource buttonClickSound;
	[SerializeField] AudioSource clappingSound;
	[SerializeField] AudioSource addMoneyPanelSound;

	public int numberOfBoost;
	[HideInInspector] public double playerBet;

	bool isWin;
	bool canShow;
	int amountOfStop = 0;
	GameObject[] winPanels;
	double amountOfAllSpins = 0;
	double fullPrize = 0;
	void Update()
	{
		ShowCountOfAllSpins();
		ButtonOn();

		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			StartCoroutine(MegaWinAppear());
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			StartCoroutine(BigWinAppear());
		}
	}

	void ShowCountOfAllSpins()
	{
		amountOfAllSpinsText.text = "Count of all spins: " + amountOfAllSpins;
	}

	//Function after end of spin.
	void ButtonOn()
	{
		HowManyMachinesIsStopped();
		OperationsAfterStopSlots();
		SetDefaultValues();
	}

	public void HowManyMachinesIsStopped()
	{
		foreach (GameObject slotMachine in slotMachineManagerScripts)
		{
			if (IsMachineStop(slotMachine) && IsMachineUnlocked(slotMachine))
			{
				amountOfStop++;
			}
		}
	}

	bool IsMachineStop(GameObject slotMachine)
	{
		return slotMachine.GetComponent<SlotMachineManager>().isStop;
	}

	bool IsMachineUnlocked(GameObject slotMachine)
	{
		return slotMachine.GetComponent<IsSlotBlockedVariable>().isSlotUnlocked;
	}

	void OperationsAfterStopSlots()
	{
		if (IsStopEquelCurrent())
		{
			CountFullPrize();
			ChargeWheel();
			BoostPrizeAmount();
			CanBigWinShow();
			CanMegaWinShow();
			MachinesCanMoving();
			UnlockButtons();
		}
	}

	public bool IsStopEquelCurrent()
	{
		return amountOfStop == getNewSloatManagerScript.currentAmountOfSlots;
	}

	void CountFullPrize()
	{
		for (int i = 0; i < getNewSloatManagerScript.currentAmountOfSlots; i++)
		{
			fullPrize += slotMachineManagerScripts[i].GetComponent<SlotMachineManager>().prize;
		}
	}

	void ChargeWheel()
	{
		chargeFill.fillAmount += (float)(fullPrize / wheelDivider);
	}

	void BoostPrizeAmount()
	{
		if (getSumBoostScripts[numberOfBoost].canCount)
		{
			getSumBoostScripts[numberOfBoost].CountThreePrizes(fullPrize);
		}
	}

	void CanBigWinShow()
	{
		if (IsFullPrizeTo() && IsFullPrizeAfter())
		{
			StartCoroutine(BigWinAppear());
		}
	}

	bool IsFullPrizeTo()
	{
		return fullPrize >= playerBet * ((multiplier - 1) * slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().multiplier) +
			slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().addendBigWin;
	}

	bool IsFullPrizeAfter()
	{
		return fullPrize < playerBet * (multiplier * slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().multiplier) +
			slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().addendBigWin;
	}

	void CanMegaWinShow()
	{
		if (IsFullPrizeSoBig())
		{
			StartCoroutine(MegaWinAppear());
		}
	}

	bool IsFullPrizeSoBig()
	{
		return fullPrize >= playerBet * (multiplier * slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().multiplier) +
			slotMachineManagerScripts[0].GetComponent<SlotMachineManager>().addendMegaWin;
	}

	void MachinesCanMoving()
	{
		foreach (GameObject slotMachine in slotMachineManagerScripts)
		{
			slotMachine.GetComponent<SlotMachineManager>().isStop = false;
		}
	}

	void UnlockButtons()
	{
		upgradeButton.enabled = true;
		spinButton.enabled = true; ;
	}

	void SetDefaultValues()
	{
		amountOfStop = 0;
		fullPrize = 0;
	}

	//Show mega win.
	public IEnumerator MegaWinAppear()
	{
		PlayMegaWinSounds();
		IsShowMegaWinDarkPanel(true);
		megaWin.transform.localScale = Vector3.zero;
		AppearMegaWinPanel();
		StartCoroutine(MegaWinParticlesManager());
		ShowFullMegaWinPrizeText();
		yield return new WaitForSeconds(timeAfterShowingText);
		HideMegaWinPanel();
		yield return new WaitForSeconds(fadeTime);
		IsShowMegaWinDarkPanel(false);
	}

	void PlayMegaWinSounds()
	{
		clappingSound.Play();
		megaWinSound.Play();
	}

	void IsShowMegaWinDarkPanel(bool onOff)
	{
		megaWinDarkBg.SetActive(onOff);
	}

	void AppearMegaWinPanel()
	{
		megaWin.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	IEnumerator MegaWinParticlesManager()
	{
		winParticles.GetComponent<UIParticle>().bakeCamera = bakeCamera;
		GameObject newPartical = Instantiate(winParticles, megawinPointOfSpawnParticles.transform);
		yield return new WaitForSeconds(timeToDestroyParticles);
		Destroy(newPartical);
	}

	void ShowFullMegaWinPrizeText()
	{
		prizeTextMegaWin.DOText("+" + fullPrize.ToString(), fadeTime + 2, true, ScrambleMode.Numerals);
	}

	void HideMegaWinPanel()
	{
		megaWin.transform.DOScale(0, fadeTime).SetEase(Ease.InExpo);
	}

	//Show big win.
	public IEnumerator BigWinAppear()
	{
		PlayBigWinSounds();
		IsShowBigWinDarkPanel(true);
		bigWin.transform.localScale = Vector3.zero;
		AppearBigWinPanel();
		StartCoroutine(BigWinParticlesManager());
		ShowFullBigWinPrizeText();
		yield return new WaitForSeconds(timeAfterShowingText);
		HideBigWinPanel();
		yield return new WaitForSeconds(fadeTime);
		IsShowBigWinDarkPanel(false);
	}

	void PlayBigWinSounds()
	{
		clappingSound.Play();
		bigWinSound.Play();
	}

	void IsShowBigWinDarkPanel(bool onOff)
	{
		bigWinDarkBg.SetActive(onOff);
	}

	void AppearBigWinPanel()
	{
		bigWin.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	IEnumerator BigWinParticlesManager()
	{
		winParticles.GetComponent<UIParticle>().bakeCamera = bakeCamera;
		GameObject newPartical = Instantiate(winParticles, bigwinPointOfSpawnParticles.transform);
		yield return new WaitForSeconds(timeToDestroyParticles);
		Destroy(newPartical);
	}

	void ShowFullBigWinPrizeText()
	{
		prizeTextBigWin.DOText("+" + fullPrize.ToString(), 3f, true, ScrambleMode.Numerals);
	}

	void HideBigWinPanel()
	{
		bigWin.transform.DOScale(0, fadeTime).SetEase(Ease.InExpo); ;
	}

	//Spin
	public void SpinSlot()
	{
		buttonClickSound.Play();
		if (IsLotsOfMoney())
		{
			playerBet = bidManagerScript.bet;
			amountOfAllSpins++;
			BlockButtons();
			playerBalanceManagerScript.ChangeMoney(-playerBet);
			PlaySpinSounds();
			StartSpin();
		}
		else
		{
			ShowAddMoneyPanel();
		}
	}

	bool IsLotsOfMoney()
	{
		return playerBalanceManagerScript.amountOfCoins >= addMoneyIfFew;
	}


	void BlockButtons()
	{
		upgradeButton.enabled = false;
		spinButton.enabled = false;
	}

	void PlaySpinSounds()
	{
		collectMoneyShowSound.Play();
		cellRotationSound.Play();
	}

	void StartSpin()
	{
		for (int i = 0; i < getNewSloatManagerScript.amountOfCells; i++)
		{
			cellRotationScripts[i].StartCoroutine("Spin");
		}
	}

	public void ShowAddMoneyPanel()
	{
		addMoneyPanelSound.Play();
		addMoneyPanelAnim.SetTrigger("Appear");
		addMoneyDarkPanel.SetActive(true);
	}
}
