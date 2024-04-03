using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private Vector3 lastPos;
    [SerializeField] private float forceToSlash;
    private RaycastHit2D hit;
    [SerializeField] GameObject trail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(trail);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Vector3.Distance(lastPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > forceToSlash)
            {
                Debug.DrawLine(lastPos, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red, 1.0f, false);
                if (Physics2D.Linecast(lastPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    hit = Physics2D.Linecast(lastPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    if (hit.collider.gameObject != null && hit.collider.gameObject.tag == "Fruit")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject != null && hit.collider.gameObject.tag == "Bomb")
                    {
                        Destroy(hit.collider.gameObject);
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
                        {
                            go.GetComponent<LvlManager>().SetDead(true);
                        }
                    }
                }
            }
            lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
