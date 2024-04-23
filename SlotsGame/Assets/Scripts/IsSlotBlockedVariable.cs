using UnityEngine;

public class IsSlotBlockedVariable : MonoBehaviour
{
	public bool isSlotUnlocked = false;

	float countOfUnlockedSlots;
	GameObject unlockedSlot;

	public void UnlockSlot()
	{
		unlockedSlot.SetActive(false);
		countOfUnlockedSlots++;
	}
}
