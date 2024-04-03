using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Fruit")
            {
                Destroy(collision.gameObject);
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
                {
                    go.GetComponent<LvlManager>().SetDead(true);
                }
            }
            else if (collision.gameObject.tag == "Bomb")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
