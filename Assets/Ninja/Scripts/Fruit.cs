using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Vector3 upForce;
    // Start is called before the first frame update
    void Start()
    {
        upForce = new Vector3 (transform.up.x + Random.Range(-0.15f, 0.15f), transform.up.y + Random.Range(-0.25f, 0.4f), 0)*500;
        GetComponent<Rigidbody2D>().AddForce(upForce);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
