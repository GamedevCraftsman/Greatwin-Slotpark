using UnityEngine;
using UnityEngine.UI;

public class PlayerBalanceManager : MonoBehaviour
{
	[SerializeField] Text playerBalanceTxt;

	bool isMoneyMax;
	float maxMoneyCount;
	public double amountOfCoins;
	GameObject[] coins;
	void Start()
	{
		playerBalanceTxt.text = amountOfCoins.ToString();
	}

	public void ChangeMoney(double price)
	{
		amountOfCoins += price;
		playerBalanceTxt.text = amountOfCoins.ToString();
	}

}
