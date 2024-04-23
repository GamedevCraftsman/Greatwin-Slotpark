using UnityEngine;

public class StartPanelUI : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject particles;
    [SerializeField] AudioSource buttonCklick;

    bool isGameStart;
    GameObject[] startButtons;
    public void Play()
    {
        buttonCklick.Play();
        startPanel.SetActive(false);
        Destroy(particles); particles = null;
    }
}
