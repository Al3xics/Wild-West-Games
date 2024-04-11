using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Fruit;
    [SerializeField] private GameObject Bomb;
    private bool timer = true;
    private float lvl = 1;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
        {
            lvl = go.GetComponent<NinjaLvlManager>().GetLvl();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timer = false;
            StartCoroutine(Spawn(Random.Range(2, 4)));
        }
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (lvl <= 1)
        {
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        else
        {
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                if (Random.Range(1, 10) <= 10 - lvl)
                {
                    Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(Bomb, gameObject.transform.position, gameObject.transform.rotation);
                }
            }
        }
        timer = true;
    }
}
