using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private float speed;
    

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private GameObject background;
    [SerializeField] private Vector2 nextPosition;

    [SerializeField] private bool alive = true;

    private void Start()
    {
        background = GameObject.Find("/Background");
        Vector2 localSize = background.GetComponent<Renderer>().bounds.size;
        minPosition = localSize * -0.5f;
        maxPosition = localSize * 0.5f;

        nextPosition = GetRandomPosition();
    }
    private Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector2 newPosition = new Vector2(randomX, randomY);
        return newPosition;
    }
    private void FixedUpdate()
    {
        if (alive)
        {
            float distance = Vector2.Distance(transform.position, nextPosition);
            if (distance >= 0.2f) 
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            }
            else
            {
                nextPosition = GetRandomPosition();
            }
        }
    }

    public void SetDeath(bool death)
    {
        alive = death;
    }
}
