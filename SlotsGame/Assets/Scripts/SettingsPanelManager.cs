using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelManager : MonoBehaviour
{
	[SerializeField] float fadeTime;
	[SerializeField] Button closeButton; 
	[SerializeField] GameObject darkPanel;
	[SerializeField] GameObject settingsPanel;
	[SerializeField] AudioSource buttonClickSound;

	bool isGamePaused;
	float timeToPause;
	void Start()
	{
		settingsPanel.transform.localScale = Vector3.zero;
		settingsPanel.SetActive(true);
	}

	public void Open()
	{
		buttonClickSound.Play();
		IsDarkPanelOpen(true);
		AppearsPanel();
	}

	void AppearsPanel()
	{
		settingsPanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

	void IsDarkPanelOpen(bool openClose)
	{
		darkPanel.SetActive(openClose);
	}

	public void ClosePanel()
	{
		buttonClickSound.Play();
		StartCoroutine(Disapear());
	}

	IEnumerator Disapear()
	{
		closeButton.enabled = false;
		settingsPanel.transform.DOScale(0f, fadeTime/2).SetEase(Ease.InExpo); 
		yield return new WaitForSeconds(fadeTime / 2);
		IsDarkPanelOpen(false);
		closeButton.enabled = true;
	}
}
