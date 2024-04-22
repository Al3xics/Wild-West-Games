using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Fruit;
    [SerializeField] private GameObject Bomb;
    private bool cd = true;
    private int timer = 1;
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
        if (cd)
        {
            cd = false;
            StartCoroutine(Spawn(timer));
        }
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (lvl > 1)
        {
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                if (Random.Range(1, 10) < 4)
                {
                    Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(Bomb, gameObject.transform.position, gameObject.transform.rotation);
                }
            }
        }
    }
}
