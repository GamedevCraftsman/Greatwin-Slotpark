using UnityEngine;
using UnityEngine.UI;

public class SoundsRegulateManager : MonoBehaviour
{
	[SerializeField] Slider volumeSlider;
	[SerializeField] AudioSource[] sounds;

	bool isLoud;
	GameObject[] soundsGO;
	void Update()
	{
		SoundChanger();
	}

	void SoundChanger()
	{
		foreach (var sound in sounds)
		{
			sound.volume = volumeSlider.value;
		}
	}
}
