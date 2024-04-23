using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChargeLuckyWheel : MonoBehaviour
{
	[SerializeField] WinManager winManagerScript;
    [SerializeField] Image fillLine;
	[SerializeField] float fadeTime;
	[Header("GameObjects")]
	[SerializeField] GameObject backPanel;
	[SerializeField] GameObject wheel;

	void Update()
	{
		ShowWheel();
	}

	void ShowWheel()
	{
		if (fillLine.fillAmount >= 1)
		{
				Show();
		}
	}

	void Show()
	{
		backPanel.SetActive(true);
		wheel.transform.localScale = Vector3.zero;
		wheel.SetActive(true);
		wheel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
		fillLine.fillAmount = 0;
	}

}
