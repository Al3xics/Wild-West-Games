using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class Pompe_LM : MonoBehaviour
{
    [SerializeField] float timeLimit = 11f;
    [SerializeField] private float timer;
    [SerializeField] private Pompe_Timer myTimer_S;

    public float Mytimer
    {
        get { return timer; }
        set { timer = value; }
    }
    [SerializeField] bool Timerflow = true;

    [SerializeField] int difficultyLevel = 1;

    [SerializeField] int numberOfClickTotalToWin;

    [SerializeField] private int numberOfClickPompe;

    public int NumberOfClickPompe
    {
        get { return numberOfClickPompe; }
        set { numberOfClickPompe = value; }
    }

    [SerializeField] private int numberOfClickRemplis;

    public int NumberOfClickRemplis
    {
        get { return numberOfClickRemplis; }
        set { numberOfClickRemplis = value; }
    }

/*    [SerializeField] int numberOfClickPompe;
    [SerializeField] int numberOfClickRemplis;*/

    [SerializeField] private int nbrClickedPompe;
    public int NbrClickedPompe
    {
        get { return nbrClickedPompe; }
        set { nbrClickedPompe = value; }
    }

    [SerializeField] private int nbrClickedRemplis;
    public int NbrClickedRemplis
    {
        get { return nbrClickedRemplis; }
        set { nbrClickedRemplis = value; }
    }

    private float diviseur = 0;

    public float Diviseur
    {
        get { return diviseur; }
    }



    void Start()
    {
        float currentDifficultyLevel = GameManager.Instance.Difficulty / 10;
        difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        if (difficultyLevel < 1) difficultyLevel = 1;

        if (difficultyLevel <= 4)
        {
            numberOfClickTotalToWin = difficultyLevel * 10;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin / 2;
        }
        else if (difficultyLevel <= 8)
        {
            numberOfClickTotalToWin = 40;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin / 2;
            timeLimit -= difficultyLevel - 3;
        }
        else
        {
            numberOfClickTotalToWin = 60;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin / 2;
            timeLimit = 4.0f;
        }

        timer = timeLimit;
    }

    void Update()
    {
        if (Timerflow)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.EndMiniGame();
                timer = 0;
            }
            if (nbrClickedPompe >= numberOfClickPompe && nbrClickedRemplis >= numberOfClickRemplis)
            {
                Debug.Log("win");
                GameManager.Instance.WinMiniGame();
                timer = 0;
            }
            myTimer_S.UpdateTimer();
        }
    }
}
