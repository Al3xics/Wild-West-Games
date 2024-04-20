using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaLvlManager : MonoBehaviour
{
    [SerializeField] private bool end = false;
    [SerializeField] private bool win = false;
    [SerializeField] private float lvl;
    [SerializeField] private int time = 10;
    [SerializeField] private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.Difficulty > 0 && GameManager.Instance.Difficulty <= 20)
        {
            lvl = 1;
        }
        else if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        {
            lvl = 2;
        }
        else if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        {
            lvl = 3;
        }
        else if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        {
            lvl = 4;
        }
        else if (GameManager.Instance.Difficulty > 80 && GameManager.Instance.Difficulty <= 100)
        {
            lvl = 5;
        }
        else
        {
            lvl = 1;
        }
        StartCoroutine(Timing(time-lvl));
    }

    // Update is called once per frame
    void Update()
    {
        if (score>=5+lvl)
        {
            win = true;
            StopCoroutine(Timing(time));
            end = true;
        }

        if (end)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                Destroy(go);
            }
            if (win)
            {
                GameManager.Instance.WinMiniGame();
                Debug.Log("WIN");
            }
            else
            {
                GameManager.Instance.EndMiniGame();
                Debug.Log("NOPE");
            }
        }
    }

    private IEnumerator Timing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        end = true;
    }

    public void SetEnd(bool b)
    {
        end = b;
    }

    public void SetScore(int i)
    {
        score = i;
    }

    public int GetScore()
    {
        return score;
    }

    public float GetLvl()
    {
        return lvl; 
    }
}
