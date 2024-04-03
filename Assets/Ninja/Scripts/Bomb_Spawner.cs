using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Bomb;
    private bool timer = true;
    private bool isRunning = false;
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
                StartCoroutine(Spawn(Random.Range(1, 5)));
            }
        }
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(Bomb, gameObject.transform.position, gameObject.transform.rotation);
        timer = true;
    }

    public void SetRunning(bool b)
    {
        isRunning = b;
    }
}
