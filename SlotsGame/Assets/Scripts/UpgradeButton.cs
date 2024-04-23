using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] float fadeTime;
    [SerializeField] Button closeButton;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject darkPanel;
	[SerializeField] AudioSource buttonClickSound;

    bool canUpgrade;
    GameObject[] upgradeSlots;
	void Start()
	{
        upgradePanel.SetActive(true);
		upgradePanel.transform.localScale = Vector3.zero;
	}

	public void OpenUpgradePanel()
    {
        buttonClickSound.Play();
        darkPanel.SetActive(true);
		ShowUpgradePanel();
	}

    void ShowUpgradePanel()
    {
		upgradePanel.transform.DOScale(1f, fadeTime).SetEase(Ease.OutExpo);
	}

    public void CloseUpgradePanel()
    {
        buttonClickSound.Play();
        StartCoroutine(HideUpgradePanel());
    }

    IEnumerator HideUpgradePanel()
    {
        closeButton.enabled = false;
        upgradePanel.transform.DOScale(0f, fadeTime / 2).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(fadeTime/2);
        upgradePanel.transform.localScale = Vector3.zero;
        closeButton.enabled = true;
        darkPanel.SetActive(false);
	}

} 
