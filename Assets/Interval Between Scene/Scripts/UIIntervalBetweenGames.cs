using Nova;
using NovaSamples.UIControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class UIIntervalBetweenGames : MonoBehaviour
{
    [Header("GameObject to disable/enable")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject winGame;

    [Header("Variables")]
    [SerializeField] private float waitingTime = 2f;

    private GameManager gameManager;
    private AdsManager adsManager;

    public GameObject GameOver => gameOver;
    public GameObject LoseGame => loseGame;
    public GameObject WinGame => winGame;

    void Start()
    {
        gameManager = GameManager.Instance;
        adsManager = GameObject.Find("Ads Manager").GetComponent<AdsManager>();
        adsManager.DisplayBanner();


        // Désactivation de tous les GameObject pour être clean
        winGame.SetActive(false);
        loseGame.SetActive(false);
        gameOver.SetActive(false);

        switch (gameManager.CurrentState)
        {
            // Win a game
            case GameManager.State.WinMiniGame:
                winGame.SetActive(true);
                UpdateLife(winGame);
                ShowScore(winGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Lose a life
            case GameManager.State.LoseMiniGame:
                loseGame.SetActive(true);
                UpdateLife(loseGame);
                ShowScore(loseGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Game Over
            case GameManager.State.LoseGame:
                gameOver.SetActive(true);
                UpdateLife(gameOver);
                ShowScore(gameOver);
                break;

            case GameManager.State.None:
                Debug.LogErrorFormat("GameManager state est : {0}", gameManager.CurrentState);
                break;
        }
    }

    public void LaunchRewardedVideo()
    {
        StartCoroutine(adsManager.WaitForRewarded());
    }

    // Retour au Menu
    public void BackToMenu()
    {
        adsManager.DestroyBanner();
        gameManager.RestartGame();
        SceneManager.LoadScene(0);
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
                lifeChildren[i].gameObject.GetComponent<UIBlock2D>().Color = Color.green;
            }
            else
            {
                lifeChildren[i].gameObject.GetComponent<UIBlock2D>().Color = Color.red;
            }
        }
    }

    // Afficher le score
    public void ShowScore(GameObject go)
    {
        if (go.name == "GameOver")
        {
            GameObject bestScoreToShow = go.transform.Find("Best Score").gameObject;
            int bestScore = gameManager.HightScore;
            bestScoreToShow.GetComponent<TextBlock>().Text = "Best Score : " + bestScore;
        }

        GameObject scoreToShow = go.transform.Find("Score").gameObject;
        int score = gameManager.Score;
        scoreToShow.GetComponent<TextBlock>().Text = "Score : " + score;
    }

    // On attend un peu puis on lance la scene suivante
    public IEnumerator WaitBeforeLaunchingScene()
    {
        yield return new WaitForSeconds(waitingTime);

        adsManager.HideBanner();
        gameManager.LoadNextMiniGame();
    }
}
