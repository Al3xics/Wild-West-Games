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
    [SerializeField] private float waitingTime = 5f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        // D�sactivation de tous les GameObject pour �tre clean
        winGame.SetActive(false);
        loseGame.SetActive(false);
        gameOver.SetActive(false);

        switch (gameManager.CurrentState)
        {
            // Win a game
            case GameManager.State.WinMiniGame:
                winGame.SetActive(true);
                UpdateLife();
                ShowScore(winGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Lose a life
            case GameManager.State.LoseMiniGame:
                loseGame.SetActive(true);
                UpdateLife();
                ShowScore(loseGame);
                StartCoroutine(WaitBeforeLaunchingScene());
                break;

            // Game Over
            case GameManager.State.LoseGame:
                gameOver.SetActive(true);
                break;

            case GameManager.State.None:
                Debug.LogErrorFormat("GameManager state est : {0}", gameManager.CurrentState);
                break;
        }
    }

    // Lancement de la publicit� r�compens� par une vie en plus pour le joueur
    public void StartPublicity()
    {
        // Le joueur lance une pubs, donc on fait une "rewarded video",
        // on lui donne 1 vie en plus

        // Si il a regarder la pub en entier, il passe au jeu suivant
        StartCoroutine(WaitBeforeLaunchingScene());
    }

    // Retour au Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
    }

    // Affiche le nombre de vie restant
    public void UpdateLife()
    {
        // R�cup�rer le nombre de vie du GameManager
        // !!!!!!!!!!!!!!!!!!!!!!!! Je ne peux pas r�cup�rer LIFE !!!!!!!!!!!!!!!!!!!!!
        // On d�sactive (ou change le sprite) des vies qui sont perdu
    }

    // Afficher le score
    private void ShowScore(GameObject go)
    {
        // On r�cup�re le game object "Title" qui est enfant de l'objet activ�
        GameObject title = go.transform.Find("Title").gameObject;
        Debug.Log(title);
        // !!!!!!!!!!!!!!!!!!!!!!!! Je ne peux pas r�cup�rer HIGHSCORE !!!!!!!!!!!!!!!!!!!!!
        // On modifie le texte de ce game object
    }

    // On attend un peu puis on lance la scene suivante
    IEnumerator WaitBeforeLaunchingScene()
    {
        yield return new WaitForSeconds(waitingTime);

        // On lance la scene suivante
        gameManager.LoadNextMiniGame();
    }
}
