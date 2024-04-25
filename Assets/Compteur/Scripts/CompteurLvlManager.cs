using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurLvlManager : MonoBehaviour
{
    [SerializeField] private NumberManager numberManager;
    [SerializeField] private float timeLimit = 10;
    [SerializeField] private Timer tm;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.Difficulty <= 20)
        {
            numberManager.interval = 19;
            tm.SetValues(timeLimit-1);
        }

        if(GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        {
            tm.SetValues(timeLimit-2);
            numberManager.interval = 16;
        }

        if(GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        {
            tm.SetValues(timeLimit-3);
            numberManager.interval = 13;
        }

        if(GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        {
            tm.SetValues(timeLimit-4);
            numberManager.interval = 10;
        }

        if(GameManager.Instance.Difficulty > 80)
        {
            tm.SetValues(timeLimit-5);
            numberManager.interval = 5;
        }
    }

    private void Update()
    {
        if (tm.GetValues() <= 0)
        {
            GameManager.Instance.EndMiniGame();
        }
    }
}
