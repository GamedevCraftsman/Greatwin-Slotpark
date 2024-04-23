using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FillXPManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] Image fillXp;
	[SerializeField] Button collectButton;
	[SerializeField] Text levelNumberText;
	[SerializeField] AudioSource levelUpAudio;
	[Header("Scripts")]
	[SerializeField] PlayerBalanceManager PlayerBalanceManager;
	[Header("Values")]
	[SerializeField] float distanceOfMoving;
	[SerializeField] float fadeTime;
	[SerializeField] int fullFillAmount;
	[SerializeField] int addend;
	public int xpDivider;
	[Header("GameObjects")]
	[SerializeField] GameObject backPanel;
	[SerializeField] GameObject chestPanel;
	[SerializeField] GameObject levelUpNumberText;
	[SerializeField] GameObject[] slotMachines;

	[HideInInspector] public double xpCount;

	bool isFilled;
	bool canMove = true;
	int amountOfFill;
	int fillCount;
	int levelNumber = 0;
	void Start()
	{
		DisableButton();
	}

	void DisableButton()
	{
		if (IsXPLineNotFill())
		{
			collectButton.enabled = false;
		}
	}

	bool IsXPLineNotFill()
	{
		return fillXp.fillAmount < fullFillAmount;
	}

	void Update()
	{
		XpFill();
	}

	void XpFill()
	{
		for (int i = 0; i < slotMachines.Length; i++)
		{
			FillManager(i);
		}
	}

	void FillManager(int i)
	{
		if (IsSlotUnlocked(i))
		{
			if (IsXPLineNotFill() && CanAddXp(i))
			{
				AddXP();
			}
			else if (IsXpLineFull())
			{
				AppearLevelUp();
				if (canMove)
				{
					canMove = true;
				}
			}
			StopAddXPOpportunity(i);
		}
	}

	bool IsSlotUnlocked(int i)
	{
		return slotMachines[i].GetComponent<IsSlotBlockedVariable>().isSlotUnlocked == true;
	}

	bool CanAddXp(int i)
	{
		return slotMachines[i].GetComponent<SlotMachineManager>().canXpAdd;
	}

	void AddXP()
	{
		fillXp.fillAmount = xpCount.ConvertTo<float>();
	}

	bool IsXpLineFull()
	{
		return fillXp.fillAmount >= fullFillAmount;
	}

	void AppearLevelUp()
	{
		IsCollectTextActive(true);
		IsCollectButtonActive(true);
		MoveLevelUpText();
	}

	void MoveLevelUpText()
	{
		if (canMove)
		{
			levelUpNumberText.transform.DOLocalMoveY(distanceOfMoving, fadeTime) //move text.
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine);
			canMove = false;
		}
	}

	void IsCollectTextActive(bool isCollect)
	{
		levelUpNumberText.SetActive(isCollect);
	}

	void IsCollectButtonActive(bool isCollect)
	{
		collectButton.enabled = isCollect;
	}

	void StopAddXPOpportunity(int i)
	{
		slotMachines[i].GetComponent<SlotMachineManager>().canXpAdd = false;
	}

	//Function for get level up prize button.
	public void CollectMoney()
	{
		ShowChestsPanel();
		IsCollectButtonActive(false);
		IsCollectTextActive(false);
		levelUpAudio.Play();
		IncreaseValue();
		SetDefaultValues();
	}

	void ShowChestsPanel()
	{
		backPanel.gameObject.SetActive(true);
		chestPanel.transform.localScale = Vector3.zero;
		chestPanel.SetActive(true);
		chestPanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void IncreaseValue()
	{
		AddLevel();
		xpDivider += addend;
	}

	void AddLevel()
	{
		levelNumber++;
		levelNumberText.text = levelNumber.ToString();
	}

	void SetDefaultValues()
	{
		fillXp.fillAmount = 0;
		xpCount = 0;
	}
}
