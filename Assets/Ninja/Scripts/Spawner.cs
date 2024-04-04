using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Fruit;
    [SerializeField] private GameObject Bomb;
    private bool timer = true;
    private bool isRunning = false;
    private int lvl = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timer)
            {
                timer = false;
                StartCoroutine(Spawn(Random.Range(4-lvl, 9-lvl)));
            }
        }
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (lvl <= 1)
        {
            Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            if (Random.Range(1, 10) <= 10-lvl)
            {
                Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
            }
            else
            {
                Instantiate(Bomb, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        timer = true;
    }

    public void SetRunning(bool b)
    {
        isRunning = b;
    }

    public void SetLvl(int i)
    {
        lvl = i;
    }
}
