using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Fruit;
    [SerializeField] private GameObject Bomb;
    private bool cd = true;
    private int timer = 0;
    private float lvl;
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
        //foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
        //{
        //    //lvl = go.GetComponent<NinjaLvlManager>().GetLvl();
        //}
        Debug.Log("Difficulty : " + lvl);
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
        for (int i = 0; i < 1+lvl; i++)
        {
            Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (lvl > 1)
        {
            for (int i = 0; i < Random.Range(1, 2+lvl); i++)
            {
                Instantiate(Bomb, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }

    public void SetLvl(float f)
    {
        lvl = f;
    }
}
