using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIIntervalBetweenGames : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject loseLife;
    [SerializeField] private GameObject actualScore;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void StartPublicity()
    {

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
    }
}
