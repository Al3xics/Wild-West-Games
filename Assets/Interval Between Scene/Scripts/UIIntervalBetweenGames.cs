using Nova;
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
    [SerializeField] private float waitingTime = 5f;

    private GameManager gameManager;
    private RewardedAds rewardedAds;

    void Start()
    {
        gameManager = GameManager.Instance;
/*        rewardedAds = GameObject.Find("Ads").GetComponent<RewardedAds>();

        rewardedAds.LoadAd();
        rewardedAds.OnUnityAdsAdLoaded("Rewarded_Android");*/


        // D�sactivation de tous les GameObject pour �tre clean
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

    // Lancement de la publicit� r�compens� par une vie en plus pour le joueur
    public void StartPublicity()
    {
        //// On lui donne 1 vie en plus
        //if (rewardedAds.OnUnityAdsShowComplete("Rewarded_Android", UnityAdsShowCompletionState.COMPLETED))
        //{

        //}

        // Si il a regarder la pub en entier, il passe au jeu suivant
        StartCoroutine(WaitBeforeLaunchingScene());
    }

    // Retour au Menu
    public void BackToMenu()
    {
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
    private void ShowScore(GameObject go)
    {
        GameObject scoreToShow = go.transform.Find("Score").gameObject;
        int score = gameManager.Score;

        scoreToShow.GetComponent<TextBlock>().Text = "Score : " + score;
    }

    // On attend un peu puis on lance la scene suivante
    IEnumerator WaitBeforeLaunchingScene()
    {
        yield return new WaitForSeconds(waitingTime);

        gameManager.LoadNextMiniGame();
    }
}
