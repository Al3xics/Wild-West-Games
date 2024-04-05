using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float upForce;
    private float hForce;
    // Start is called before the first frame update
    void Start()
    {
        hForce = Random.Range(-500, 500);
        GetComponent<Rigidbody2D>().AddForce(new Vector3(hForce, upForce, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
