using UnityEngine;

public class CollectAFewMoney : MonoBehaviour
{
	[SerializeField] PlayerBalanceManager playerBalanceManager;
	[SerializeField] Animator addMoneyAnim;
	[SerializeField] GameObject addMoneyBackPanel;
	[SerializeField] AudioSource buttonClick;
	[SerializeField] double amountOfMoney;

	double timeToCollect;
	bool isCollected;

	public void AddMoney()
	{
		buttonClick.Play();
		playerBalanceManager.ChangeMoney(amountOfMoney);
		addMoneyAnim.SetTrigger("Hide");
		addMoneyBackPanel.SetActive(false);
	}
}
