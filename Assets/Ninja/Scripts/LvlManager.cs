using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private bool end = false;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool win = false;
    [SerializeField] private int lvl;
    private bool timer = true;
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
            if (timer)
            {
                timer = false;
                StartCoroutine(Timing(20));
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
        timer = true;
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
