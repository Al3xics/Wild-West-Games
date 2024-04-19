using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difference_LM : MonoBehaviour
{
    [SerializeField] float timeLimit = 11f;
    [SerializeField] float timer;
    [SerializeField] private Timer_Color myTimer_S;
    [SerializeField] private Instanciate_Button myInstanciateGame_S;

    public float Mytimer
    {
        get { return timer; }
        set { timer = value; }
    }

    [SerializeField] bool Timerflow = true;

    [SerializeField] int difficultyLevel = 1;
    void Start()
    {
        float currentDifficultyLevel = GameManager.Instance.Difficulty / 10;
        difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        if (difficultyLevel < 1) difficultyLevel = 1;

        if (difficultyLevel <= 4)
        {
            myInstanciateGame_S.Make_Game(difficultyLevel);
        }
        else if (difficultyLevel <= 8)
        {
            myInstanciateGame_S.Make_Game(difficultyLevel);

            timeLimit -= difficultyLevel - 3;
        }
        else
        {
            myInstanciateGame_S.Make_Game(difficultyLevel);
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
/*            if ()
            {
                Debug.Log("win");
                GameManager.Instance.WinMiniGame();
                timer = 0;
            }*/
            myTimer_S.UpdateTimer();
        }
    }
}
