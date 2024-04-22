using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurLvlManager : MonoBehaviour
{
    [SerializeField] NumberManager numberManager;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.Difficulty <= 20)
        {
            numberManager.interval = 19;
        }

        if(GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        {
            numberManager.interval = 16;
        }

        if(GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        {
            numberManager.interval = 13;
        }

        if(GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        {
            numberManager.interval = 10;
        }

        if(GameManager.Instance.Difficulty > 80 && GameManager.Instance.Difficulty <= 100)
        {
            numberManager.interval = 5;
        }
    }

}
