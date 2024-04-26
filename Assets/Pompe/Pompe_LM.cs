using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class Pompe_LM : MonoBehaviour
{
    [SerializeField] float timeLimit = 11f;
    [SerializeField] private Timer tm;

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
            numberOfClickTotalToWin = difficultyLevel * 5;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin ;
        }
        else if (difficultyLevel <= 8)
        {
            numberOfClickTotalToWin = 20;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin ;
            timeLimit -= difficultyLevel - 3;
        }
        else
        {
            numberOfClickTotalToWin = 30;
            diviseur = 1f / numberOfClickTotalToWin;
            numberOfClickPompe = numberOfClickTotalToWin / 2;
            numberOfClickRemplis = numberOfClickTotalToWin;
            timeLimit = 5.0f;
        }

        tm.SetValues(timeLimit);
    }

    void Update()
    {
        if (tm.GetValues() <= 0)
        {
            GameManager.Instance.EndMiniGame();
        }
        if (nbrClickedPompe >= numberOfClickPompe && nbrClickedRemplis >= numberOfClickRemplis)
        {
            GameManager.Instance.WinMiniGame();
        }
    }
}
