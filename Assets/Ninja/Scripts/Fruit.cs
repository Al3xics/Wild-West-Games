using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Vector3 upForce;
    // Start is called before the first frame update
    void Start()
    {
        upForce = new Vector3(transform.up.x + Random.Range(-0.4f, 0.4f), transform.up.y + Random.Range(0f, 0.5f), 0) * 200;
        transform.rotation = new Quaternion(upForce.x, upForce.y, upForce.z, upForce.z);
        GetComponent<Rigidbody2D>().AddForce(upForce);
    }

    private void Update()
    {
        if (GetComponent<ParticleSystem>().isEmitting == false)
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }

    public void Particles()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<ParticleSystem>().Play();
    }
}
