using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject games;
    [SerializeField] private AudioClip InGameClip;

    // L'objet en haut de la pile est l'objet actuel sur lequel on se situe
    private Stack<GameObject> objectStack = new();
    private GameManager gameManager;
    private MusicManager musicManager;
    private AdsManager adsManager;

    void Start()
    {
        objectStack.Push(menu);
        gameManager = GameManager.Instance;
        adsManager = GameObject.Find("Ads Manager").GetComponent<AdsManager>();
        musicManager = MusicManager.Instance;
        adsManager.LaunchBanner();
        adsManager.HideBanner();
    }

    public void PlayGameButton()
    {
        GameManager.Instance.LoadNextMiniGame();
        musicManager.audioSource.clip = InGameClip;
        musicManager.audioSource.Play();
    }

    public void SettingsButton()
    {
        objectStack.Push(settings);
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void GamesButton()
    {
        objectStack.Push(games);
        menu.SetActive(false);
        games.SetActive(true);
    }

    public void LaunchInterstitialVideo()
    {
        adsManager.LaunchInterstitial();
    }

    public void ProgressionButton(GameObject panelProgression)
    {
        objectStack.Push(panelProgression);
        int bestScore = PlayerPrefs.GetInt("HighScore");
        panelProgression.GetComponentInChildren<TextMeshProUGUI>().text = "Best Score : " + bestScore;
        panelProgression.SetActive(true);
    }

    public void AboutUsButton(GameObject panelABoutUs)
    {
        objectStack.Push(panelABoutUs);
        panelABoutUs.SetActive(true);
    }

    public void BackButton()
    {
        if (objectStack.Count > 0)
        {
            objectStack.Pop().SetActive(false);
            objectStack.Peek().SetActive(true);
        }
        else
        {
            Debug.LogWarning("Il n'y a pas d'objet précédent à activer.");
        }
    }
}
