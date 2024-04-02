using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Fruit;
    private float spawnPointOffset;
    private bool timer = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timer = false;
            StartCoroutine(Spawn(Random.Range(1,5)));
        }
        
    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(Fruit, gameObject.transform.position, gameObject.transform.rotation);
        timer = true;
    }
}
