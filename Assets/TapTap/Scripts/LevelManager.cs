using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject cockroachPrefab;
    [SerializeField] int cockroachsAmount = 1;
    [SerializeField] int cockroachsDead = 0;
    [SerializeField] TextBlock winText;
    [SerializeField] TextBlock loseText;

    [SerializeField] float timeLimit = 60f;
    [SerializeField] float timer;
    [SerializeField] bool Timerflow = true;
    [SerializeField] TextBlock timerText;

    private void Start()
    {
        //SpawnCockroach
        if (cockroachPrefab != null)
        {
            for (int i = 0; i < cockroachsAmount; i++)
            {
            
                GameObject cockroach = Instantiate(cockroachPrefab, new Vector2 (0.0f,0.0f), Quaternion.identity );

            }
        }

        //Timer start
        timer = timeLimit;
       
    }

    void Update()
    {
        //Timer update
        if (Timerflow)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (loseText != null)
                {
                    loseText.gameObject.SetActive(true);
                    Timerflow = false;
                }
                timer = 0;
            }
            UpdateTimerUI();

        }
        //Mouse Imput
        if (Input.GetMouseButtonDown(0) && timer>0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D colliderHit = Physics2D.OverlapPoint(mousePosition);
            if(colliderHit != null && colliderHit.GetComponent<Cockroach>()) 
            {
                colliderHit.GetComponent<Cockroach>().SetAlive(false);
                cockroachsDead++;
            }
        }
        if(cockroachsDead >= cockroachsAmount)
        {
            if(winText != null)
            {
                 winText.gameObject.SetActive(true);
                Timerflow = false;
            }
        }
    }

    void UpdateTimerUI()
    {
       
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

       
        timerText.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
