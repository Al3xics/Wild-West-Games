using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difference_LM : MonoBehaviour
{
    [SerializeField] float timeLimit = 8f;
    [SerializeField] Timer tm;
    [SerializeField] private Instanciate_Button myInstanciateGame_S;

    [SerializeField] int difficultyLevel = 1;
    void Start()
    {
        float currentDifficultyLevel = GameManager.Instance.Difficulty / 10;
        difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        if (difficultyLevel < 1) difficultyLevel = 1;

        if (difficultyLevel <= 4)
        {
            myInstanciateGame_S.Make_Game(difficultyLevel + 2);
        }
        else if (difficultyLevel <= 8)
        {
            myInstanciateGame_S.Make_Game(difficultyLevel + 2);

            timeLimit -= difficultyLevel - 3;
        }
        else
        {
            myInstanciateGame_S.Make_Game(difficultyLevel);
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
    }
}
