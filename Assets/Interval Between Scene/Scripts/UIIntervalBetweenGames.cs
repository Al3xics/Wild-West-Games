using NovaSamples.UIControls;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class UIIntervalBetweenGames : MonoBehaviour
{
    [Header("GameObject to disable/enable")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject buttonPubs;
    [SerializeField] private Sprite lifeWin;
    [SerializeField] private Sprite lifeLose;

    [Header("Variables")]
    [SerializeField] private float waitingTime = 2f;

    private GameManager gameManager;
    private AdsManager adsManager;
    private bool lostPreviousRound = false;


    public GameObject GameOver => gameOver;
    public GameObject LoseGame => loseGame;
    public GameObject WinGame => winGame;

    void Start()
    {
        gameManager = GameManager.Instance;
/*        adsManager = GameObject.Find("Ads Manager").GetComponent<AdsManager>();
        adsManager.DisplayBanner();*/


        // Désactivation de tous les GameObject pour être clean
        winGame.SetActive(false);
        loseGame.SetActive(false);
        gameOver.SetActive(false);

        
        switch (gameManager.CurrentState)
        {
            // Win a game
            case GameManager.State.WinMiniGame:
                
                SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.WinSequence);
                winGame.SetActive(true);
                UpdateLife(winGame);
                ShowScore(winGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Lose a life
            case GameManager.State.LoseMiniGame:
                SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.LoseSequence);
                loseGame.SetActive(true);
                UpdateLife(loseGame);
                ShowScore(loseGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Game Over
            case GameManager.State.LoseGame:
                SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.GameOverSequence);
                gameOver.SetActive(true);
                UpdateLife(gameOver);
                ShowScore(gameOver);
                GameManager.Instance.isTraining = false;
                CanWeWatchRewarded();
                break;

            case GameManager.State.None:
                Debug.LogErrorFormat("GameManager state est : {0}", gameManager.CurrentState);
                break;
        }
    }

    private void CanWeWatchRewarded()
    {
        if (adsManager.AlreadyWatchedPubs)
        {
            buttonPubs.SetActive(false);
        }
    }

    public void LaunchRewardedVideo()
    {
        StartCoroutine(adsManager.WaitForRewarded());
    }

    // Retour au Menu
    public void BackToMenu()
    {
        //adsManager.DestroyBanner();
        gameManager.RestartGame();
        //SceneManager.LoadScene(0); // pas besoin car c'est fait dans RestartGame
    }

    // Affiche le nombre de vie restant
    public void UpdateLife(GameObject go)
    {
        int life = gameManager.Life;
        List<GameObject> lifeChildren = new();

        foreach (Transform childTransform in go.transform)
        {
            if (childTransform.name == "Life")
            {
                lifeChildren.Add(childTransform.gameObject);
            }
        }

        for (int i = 0; i < lifeChildren.Count; i++)
        {
            if (i < life)
            {
                lifeChildren[i].gameObject.GetComponent<Image>().sprite = lifeWin;
            }
            else
            {

                if (i == life)
                {
                    if (gameManager.lostprevious)
                        lifeChildren[i].gameObject.GetComponent<Animator>().enabled = true;
                    else
                        lifeChildren[i].gameObject.GetComponent<Image>().sprite = lifeLose;
                }
                else
                {
                    lifeChildren[i].gameObject.GetComponent<Image>().sprite = lifeLose;
                }
            }
        }

        lostPreviousRound = (life < gameManager.Life);
    }

    // Afficher le score
    public void ShowScore(GameObject go)
    {
        if (go.name == "GameOver")
        {
            GameObject bestScoreToShow = go.transform.Find("Best Score").gameObject;
            int bestScore;
            if (GameManager.Instance.isTraining)
            {
                string name = "HighScore" + GameManager.Instance.miniGameTrain;
                bestScore = PlayerPrefs.GetInt(name);
            }
            else
            {
                bestScore = PlayerPrefs.GetInt("HighScore");
            }
            bestScoreToShow.GetComponent<TMP_Text>().text = "Best Score : " + bestScore;
        }

        GameObject scoreToShow = go.transform.Find("Score").gameObject;
        int score = gameManager.Score;
        scoreToShow.GetComponent<TMP_Text>().text = "Score : " + score;
    }

    // On attend un peu puis on lance la scene suivante
    public IEnumerator WaitBeforeLaunchingScene()
    {
        yield return new WaitForSeconds(waitingTime);

        //adsManager.HideBanner();
        
        gameManager.LoadNextMiniGame();
    }
}
