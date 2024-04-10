using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Vector3 upForce;
    private float hForce;
    // Start is called before the first frame update
    void Start()
    {
        hForce = Random.Range(-200, 200);
        //upForce.y = gameObject.transform.up + Random.Range(-10, 10);
        upForce = new Vector3 (gameObject.transform.up.x, gameObject.transform.up.y, 0)*500;
        GetComponent<Rigidbody2D>().AddForce(upForce);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
