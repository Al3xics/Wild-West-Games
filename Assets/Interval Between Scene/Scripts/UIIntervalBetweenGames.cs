using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIIntervalBetweenGames : MonoBehaviour
{
    [Header("GameObject to disable/enable")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject winGame;

    [Header("Variables")]
    [SerializeField] private float waitingTime;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        // Désactivation de tous les GameObject pour être clean
        winGame.SetActive(false);
        loseGame.SetActive(false);
        gameOver.SetActive(false);

        switch (gameManager.CurrentState)
        {
            // Win a game
            case GameManager.State.WinMiniGame:
                winGame.SetActive(true);
                UpdateLife();

                // Afficher le score

                // On attend un peu puis on lance la scene suivante
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Lose a life
            case GameManager.State.LoseMiniGame:
                loseGame.SetActive(true);
                UpdateLife();

                // Afficher le score

                // On attend un peu puis on lance la scene suivante
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Game Over
            case GameManager.State.LoseGame:
                gameOver.SetActive(true);
                break;
        }
    }

    public void StartPublicity()
    {
        // Le joueur lance une pubs, donc on fait une "rewarded video",
        // on lui donne 1 vie en plus

        // Si il a regarder la pub en entier, il passe au jeu suivant
        StartCoroutine(WaitBeforeLaunchingScene());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
    }

    public void UpdateLife()
    {
        // Récupérer le nombre de vie du GameManager
        // on désactive (ou change le sprite) des vies qui sont perdu
    }

    IEnumerator WaitBeforeLaunchingScene()
    {
        yield return new WaitForSeconds(waitingTime);
    }
}
