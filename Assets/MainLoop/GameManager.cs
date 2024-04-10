using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum State
    {
        None,
        WinMiniGame,
        LoseMiniGame,
        LoseGame
    }

    public State currentState;

    public State CurrentState
    {
        get { return currentState; }
    }

    [SerializeField] private int NumberOfMiniGame = 0;
    [SerializeField] private int currentMiniGame = -1;

    [SerializeField] private float difficulty = 0;

    public float Difficulty
    {
        get { return difficulty; }
    }

    [SerializeField] private int hightScore;
    public int HightScore
    {
        get { return hightScore; }
    }

    [SerializeField] private int life;
    public int Life
    {
        get { return life; }
    }

    private int score;

    public int Score
    {
        get { return score; }
    }

    private List<bool> games;
    [SerializeField] private List<string> gamesName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            games = new List<bool>();
            for (int i = 0; i < NumberOfMiniGame; i++)
            {
                games.Add(false);
            }
            //currentState = State.None;
            LoadData();
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void Start()
    {
        StartCoroutine(SaveDataLoop());
    }

    public void setGamesName(string _gameName)
    {
        gamesName.Add(_gameName);
    }

    public void LoadNextMiniGame(int num = -1)
    {
        if (num == -1)
        {
            while (num == currentMiniGame || num == -1)
                num = Random.Range(0, NumberOfMiniGame);
        }
        SceneManager.LoadScene(num + 1);
    }

    public void WinMiniGame()
    {
        score += 1;
        currentState = State.WinMiniGame;
        if (difficulty < 100)
            difficulty += 1;
        //loadscene between menu
        LoadNextMiniGame();
    }

    public void RestartGame()
    {
        life = 3;
        score = 0;
        difficulty = 0;
        SceneManager.LoadScene(0);
    }

    public void ResetAllData()
    {
        hightScore = 0;
        for (int i = 0; i < games.Count ; i++)
        {
            games[i] = false;
        }
        RestartGame();
        _SaveData();
    }

    public bool EndMiniGame()
    {
        life -= 1;
        if (life == 0)
        {
            if (score > hightScore)
                hightScore = score;
            score = 0;
            life = 3;
            currentState = State.LoseGame;
            //loadscene between menu
            return false;
        }
        if (difficulty < 100)
            difficulty += 1;
        currentState = State.LoseMiniGame;
        //loadscene between menu
        return true;

    }

    private void _SaveData()
    {
        PlayerPrefs.SetFloat("Difficulty", difficulty);
        PlayerPrefs.SetInt("HighScore", hightScore);
        string a = "";
        for (int i = 0; i < NumberOfMiniGame; i++)
        {
            if (games[i])
                a += '1';
            else
                a += '0';
        }
        PlayerPrefs.SetString("MiniGame", a);
    }
    IEnumerator SaveDataLoop()
    {
        _SaveData();
        yield return new WaitForSeconds(5);
        StartCoroutine(SaveDataLoop());
    }

    private void LoadData()
    {
        difficulty = PlayerPrefs.GetFloat("Difficulty", 0);
        hightScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
        string a = "";
        for (int i = 0; i < NumberOfMiniGame; i++)
        {
            a += '0';
        }
        string pref = PlayerPrefs.GetString("MiniGame");
        if (pref.Length != NumberOfMiniGame)
            pref = "";
        if (pref == "") 
            pref = a;
        for (int i = 0; i < NumberOfMiniGame; i++)
        {
            if (pref[i] == 0)
                games[i] = false;
            else
                games[i] = true;
        }
    }

}
