using System;
using System.Collections;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private GameObject background;
    [SerializeField] private Vector2 nextPosition;

    [SerializeField] private bool alive = true;

    [SerializeField] private bool toCatch = false;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove = true;
    [SerializeField] private float stopTime = 1.0f;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private ParticleSystem particle;


    private void Start()
    {
        background = GameObject.Find("Panel");

        particle = GetComponent<ParticleSystem>();

        Vector2 localSize = new Vector2(background.GetComponent<RectTransform>().rect.width, background.GetComponent<RectTransform>().rect.height);

        localSize /= 100;

        minPosition = localSize * -0.5f;
        maxPosition = localSize * 0.5f;
        
        
        
        nextPosition = GetRandomPosition();
        rb = GetComponent<Rigidbody2D>();


        //Change Color
        if (toCatch)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    private Vector2 GetRandomPosition()
    {
        float randomX = UnityEngine.Random.Range(minPosition.x, maxPosition.x);
        float randomY = UnityEngine.Random.Range(minPosition.y, maxPosition.y);
        Vector2 newPosition = new Vector2(randomX, randomY);
        return newPosition;
    }
    private void FixedUpdate()
    {
        if (alive)
        {
            float distance = Vector2.Distance(rb.position, nextPosition);
            if (distance >= 0.2f && canMove ) 
            {
                rb.position = Vector2.MoveTowards(rb.position, nextPosition, speed * Time.deltaTime);
                
            }
            else
            {
               if(canMove)
                {
                    StartCoroutine(WaitAndMove());   

                }
            }
        }
    }

    public void SetAlive(bool death)
    {
        alive = death;
        SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.SplashInsect);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        rb.isKinematic = true;
        particle.Play();
       
    }

    public void SetCatch()
    {
        toCatch = true;
    }

    public bool GetCatch()
    {
        return toCatch;
    }

   IEnumerator WaitAndMove()
    {
        canMove = false;
        nextPosition = GetRandomPosition();
        yield return new WaitForSeconds(stopTime);
        canMove = true;
        
    }

    private void Update()
    {
        if (!canMove)
        {
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, nextPosition);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, rot, Time.deltaTime * turnSpeed);

        }

        if(!particle.isEmitting)
        {
            particle.Stop();
        }
    }
}
