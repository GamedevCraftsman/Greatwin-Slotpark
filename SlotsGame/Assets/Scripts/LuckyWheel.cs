using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class LuckyWheel : MonoBehaviour
{
	[Header("General")]
	[SerializeField] List<string> prizes;
	[SerializeField] Button spinButton;
	[Header("Scripts")]
	[SerializeField] PlayerBalanceManager playerBalanceManagerScript;
	[Header("Values")]
	[SerializeField] float rotationSpeed;
	[SerializeField] float rotationTimeMaxSpeed;
	[SerializeField] float accelerationTime;
	[SerializeField] int numberOfSpins;
	[Header("GameObjects")]
	[SerializeField] GameObject wheel;
	[SerializeField] GameObject backPanel;
	[SerializeField] GameObject allWheel;
	[Header("AudoiSources")]
	[SerializeField] AudioSource spinWheelSound;

	int randomSector;
	bool isSpin = false;
	bool canSpin = false;
	float slowdownTime;

	float maxAngle;
	float minAngle;
	void Update()
	{
		LetSpin();
	}

	void LetSpin()
	{
		if (!isSpin && canSpin)
		{
			StartCoroutine(SpinWheel());
		}
	}

	IEnumerator SpinWheel()
	{
		spinWheelSound.Play();
		Setwin();
		isSpin = true;
		float elapsedTime = 0f;
		float rotSpeed;

		while (elapsedTime < accelerationTime)
		{
			rotSpeed = Mathf.Lerp(0, rotationSpeed, elapsedTime / accelerationTime);
			wheel.transform.rotation *= Quaternion.Euler(0, 0, rotSpeed * Time.deltaTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		elapsedTime = 0f;
		while (elapsedTime < rotationTimeMaxSpeed)
		{
			wheel.transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		float distance = (numberOfSpins * 360f) + UnityEngine.Random.Range(minAngle + 2, maxAngle - 2);
		slowdownTime = (2 * distance) / rotationSpeed;
		float slowdown = rotationSpeed / slowdownTime;
		rotSpeed = rotationSpeed;

		elapsedTime = 0f;
		while (elapsedTime < slowdownTime)
		{
			rotSpeed -= slowdown * Time.deltaTime;
			wheel.transform.rotation *= Quaternion.Euler(0, 0, rotSpeed * Time.deltaTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		spinWheelSound.Stop();
		playerBalanceManagerScript.ChangeMoney(Convert.ToInt32(prizes[randomSector]));
		Debug.Log("prize: " + Convert.ToInt32(prizes[randomSector]));

		yield return new WaitForSeconds(2);
		allWheel.transform.DOScale(0f, 1).SetEase(Ease.InExpo);
		yield return new WaitForSeconds(1);
		backPanel.SetActive(false);
		wheel.transform.rotation = Quaternion.Euler(0, 0, 0);

		spinButton.interactable = true;
		isSpin = false;
		canSpin = false;
	}


	private void Setwin()
	{
		randomSector = UnityEngine.Random.Range(0, prizes.Count);
		Debug.Log("WIN " + prizes[randomSector] + " | index = " + randomSector);

		maxAngle = 360f / prizes.Count * (randomSector + 1);
		minAngle = 360f / prizes.Count * randomSector;
	}

	public void Spin()
	{
		canSpin = true;
		spinButton.interactable = false;
	}
}
