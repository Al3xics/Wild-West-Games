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
        lvl = GameManager.Instance.Difficulty;
        StartCoroutine(Timing(time));
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
