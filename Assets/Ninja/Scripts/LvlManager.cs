using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private bool dead = false;
    [SerializeField] private bool isRunning = false;
    private Fruit_Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            isRunning = false;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                Destroy(go);
            }
        }
        if (isRunning)
        {
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                spawner = go.GetComponent<Fruit_Spawner>();
                spawner.SetRunning(true);
            }
        }
        else
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                spawner = go.GetComponent<Fruit_Spawner>();
                spawner.SetRunning(false);
            }
        }
    }

    public void SetDead(bool b)
    {
        dead = b;
    }
}
