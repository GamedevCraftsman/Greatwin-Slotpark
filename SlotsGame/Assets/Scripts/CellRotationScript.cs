using System.Collections;
using UnityEngine;
public enum SlotValue
{
	Cherry,
	BlueHeart,
	Lance,
	Strawberry,
	Bell,
	Crown
}

public class CellRotationScript : MonoBehaviour
{
	[Header("General")]
	[SerializeField] GameObject cell;
	[SerializeField] AudioSource endRotation;
	[Header("Scripts")]
	[SerializeField] SlotMachineManager slotMachineManagerScript;
	[SerializeField] CheckResultVariable checkResultVariableScript;
	[Header("Values")]
	[SerializeField] float rotationSpeed;
	[SerializeField] float diapazone;
	[SerializeField] int firstNumOfRandomRange;
	[SerializeField] int secondNumOfRandomRange;
	[SerializeField] float stoppedSpeed;
	[SerializeField] float speedDivider;
	[SerializeField] float downIconsPosition;
	[SerializeField] float intervalBetweenTwoIcons;
	[SerializeField] float timeIntervalToTighten;
	[SerializeField] float tightenSpeed;

	public SlotValue[] stoppedSlot;
	public bool isDoubleSlot = false;
	double numberOf;
	bool isOn;
	public float intervalOfTime;
	bool isOff;

	float overallSpeed = 0;
	int oddValue;
	public IEnumerator Spin()
	{
		CountRandomSpeed();

		while (IsEnoughSpeedToSpin())
		{
			DecreaseSpeed();
			MoveCell();
			CheckCellPosition();
			yield return new WaitForSeconds(intervalOfTime);
		}
		StartCoroutine(EndSpin());
		yield return null;
	}

	void CountRandomSpeed()
	{
		oddValue = Random.Range(firstNumOfRandomRange, secondNumOfRandomRange);
		overallSpeed += rotationSpeed + oddValue;
	}

	bool IsEnoughSpeedToSpin()
	{
		return overallSpeed >= stoppedSpeed;
	}

	void DecreaseSpeed()
	{
		overallSpeed /= speedDivider;
	}

	void MoveCell()
	{
		cell.transform.Translate(Vector2.up * Time.deltaTime * -overallSpeed); ;
	}

	void CheckCellPosition()
	{
		if (IsCellInTheLowestPosition())
		{
			ReturnCellToStartPosition();
		}
	}

	bool IsCellInTheLowestPosition()
	{
		return cell.transform.localPosition.y <= downIconsPosition;
	}

	void ReturnCellToStartPosition()
	{
		cell.transform.localPosition = new Vector2(cell.transform.localPosition.x, 0);
	}
	//End spin.
	IEnumerator EndSpin()
	{
		while (IsEnoughSpeedToTighten())
		{
			for (float i = 0; i >= downIconsPosition; i -= intervalBetweenTwoIcons)
			{
				if (IsCellStopBetweenTwoIcons(i))
				{
					TightenCell(i);
					if (IsPositionEqual(i))
					{
						StopCell();
						break;
					}
				}
			}
			DecreaseSpeed();
			yield return new WaitForSeconds(timeIntervalToTighten);
		}
		StopCell();
		PlayStopSound();
		CheckResults();
		yield return null;
	}

	bool IsEnoughSpeedToTighten()
	{
		return overallSpeed >= tightenSpeed;
	}

	bool IsCellStopBetweenTwoIcons(float i)
	{
		return cell.transform.localPosition.y <= i && cell.transform.localPosition.y >= (i - intervalBetweenTwoIcons);
	}

	void TightenCell(float i)
	{
		cell.transform.localPosition = Vector2.Lerp(cell.transform.localPosition, new Vector2(cell.transform.localPosition.x, i), Time.deltaTime * 10);
	}

	bool IsPositionEqual(float i)
	{
		return new Vector2(cell.transform.localPosition.x, cell.transform.localPosition.y) == new Vector2(cell.transform.localPosition.x, i);
	}

	void StopCell()
	{
		overallSpeed = 0;
	}

	void PlayStopSound()
	{
		endRotation.Stop();
		endRotation.Play();
	}

	//Check results.
	void CheckResults()
	{
		if (isDoubleSlot)
		{
			GiveANameUpperSlot(1);
		}
		GiveANameDownSlot(0);
		slotMachineManagerScript.WaitResults();
	}

	void GiveANameUpperSlot(int numOfNameArr)
	{
		if (IsCountCellPosition(0))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(1))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(2))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(3))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(4))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(5))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(6))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(7))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Bell;
		}
		else if (IsCountCellPosition(8))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(9))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(10))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Strawberry;
		}
		else if (IsCountCellPosition(11))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(12))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(13))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(14))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(15))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
	}

	void GiveANameDownSlot(int numOfNameArr)
	{
		if (IsCountCellPosition(0))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(1))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(2))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(3))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(4))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(5))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(6))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(7))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(8))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Bell;
		}
		else if (IsCountCellPosition(9))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(10))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Lance;
		}
		else if (IsCountCellPosition(11))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Strawberry;
		}
		else if (IsCountCellPosition(12))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(13))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
		else if (IsCountCellPosition(14))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Crown;
		}
		else if (IsCountCellPosition(15))
		{
			stoppedSlot[numOfNameArr] = SlotValue.BlueHeart;
		}
		else if (IsCountCellPosition(16))
		{
			stoppedSlot[numOfNameArr] = SlotValue.Cherry;
		}
	}
	bool IsCountCellPosition(int arrayNumber)
	{
		return IsCellpositionFrom(arrayNumber) && IsCellPositionTo(arrayNumber);
	}

	bool IsCellpositionFrom(int arrayNumber)
	{
		return cell.transform.localPosition.y <= checkResultVariableScript.iconsPositions[arrayNumber] + diapazone;
	}

	bool IsCellPositionTo(int arrayNumber)
	{
		return cell.transform.localPosition.y >= checkResultVariableScript.iconsPositions[arrayNumber] - diapazone;
	}
}