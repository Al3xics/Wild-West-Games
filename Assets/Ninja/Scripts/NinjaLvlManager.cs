using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaLvlManager : MonoBehaviour
{
    [SerializeField] private bool end = false;
    [SerializeField] private bool isRunning = true;
    [SerializeField] private bool win = false;
    [SerializeField] private int lvl;
    [SerializeField] private int time = 10;
    private bool timerBool = true;
    // Start is called before the first frame update
    void Start()
    {
        SetLvl(lvl);
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            isRunning = false;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                Destroy(go);
            }

            if(win)
            {
                Debug.Log("WIN");
            }
            else
            {
                Debug.Log("NOPE");
            }
        }
        if (isRunning)
        {
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                go.GetComponent<Spawner>().SetRunning(true);
            }
            if (timerBool)
            {
                timerBool = false;
                StartCoroutine(Timing(time));
            }
        }
        else
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                go.GetComponent<Spawner>().SetRunning(false);
            }
        }
    }

    private IEnumerator Timing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        end = true;
        win = true;
        timerBool = true;
    }

    public void SetEnd(bool b)
    {
        end = b;
    }

    public void SetLvl(int i)
    {
        lvl = i;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            go.GetComponent<Spawner>().SetLvl(lvl);
        }
    }

    public int GetLvl()
    {
        return lvl; 
    }
}
