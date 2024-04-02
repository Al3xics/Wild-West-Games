using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject cockroachs;
    [SerializeField] int cockroachsAmount = 1;

    private void Start()
    {
        for (int i = 0; i < cockroachsAmount; i++)
        {
            GameObject cockroach = Instantiate(cockroachs);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D colliderHit = Physics2D.OverlapPoint(mousePosition);
            if(colliderHit != null && colliderHit.GetComponent<Cockroach>()) 
            {
                colliderHit.GetComponent<Cockroach>().SetDeath(false);
            }
            

        }
    }
}
