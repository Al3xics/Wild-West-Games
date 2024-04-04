using Nova;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum GameState
    {
        Win,
        Lose
    }
    [SerializeField] GameObject cockroachPrefab;
    [SerializeField] int cockroachsAmount = 1;
    [SerializeField] int cockroachsToWin = 1;
    [SerializeField] int cockroachsDead = 0;
    [SerializeField] List<int> spawnNumbers;
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
            for (int i = 0; i < cockroachsToWin; i++)
            {
                int spawnNumber;
                do
                {
                    spawnNumber = Random.Range(0, cockroachsAmount);
                } while (spawnNumbers.Contains(spawnNumber));
                spawnNumbers.Add(spawnNumber);
            }
            for (int i = 0; i < cockroachsAmount; i++)
            {
                GameObject cockroach = Instantiate(cockroachPrefab, new Vector2 (0.0f,0.0f), Quaternion.identity,transform );
                if(spawnNumbers.Contains(i))
                {
                    cockroach.GetComponent<Cockroach>().SetCatch();
                }
                
            }
        }

        //Timer start
        timer = timeLimit;
       
    }

    private void Update()
    {
        //Timer update
        if (Timerflow)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (loseText != null)
                {
                    EndScreen(GameState.Lose);
                }
                timer = 0;
            }
            UpdateTimerUI();

        }
        //Mouse Input
        //if (Input.GetMouseButtonDown(0) && timer>0)
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Collider2D colliderHit = Physics2D.OverlapPoint(mousePosition);
        //    if(colliderHit != null && colliderHit.GetComponent<Cockroach>()) 
        //    {
        //        if (colliderHit.GetComponent<Cockroach>().GetCatch())
        //        {
        //            colliderHit.GetComponent<Cockroach>().SetAlive(false);
        //            cockroachsDead++;
        //        }
        //        else
        //        {
        //            EndScreen(GameState.Lose);
        //        }
        //    }
        //}

        //TouchInput
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && timer > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            Collider2D colliderHit = Physics2D.OverlapPoint(worldPosition);
            if (colliderHit != null && colliderHit.GetComponent<Cockroach>())
            {
                if (colliderHit.GetComponent<Cockroach>().GetCatch())
                {
                    colliderHit.GetComponent<Cockroach>().SetAlive(false);
                    cockroachsDead++;
                }
                else
                {
                    EndScreen(GameState.Lose);
                }
            }
        }

        //WinCheck
        if (cockroachsDead >= cockroachsToWin)
        {
            if (winText != null)
            {
                EndScreen(GameState.Win);
            }
        }
    }

    private void UpdateTimerUI()
    {
       
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

       
        timerText.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ClearScreen()
    {
        foreach (Transform cockRoach in transform)
        {
            
            cockRoach.gameObject.SetActive(false);
        }
    }
    private void EndScreen(GameState state)
    {
        switch (state)
        {
            case GameState.Win:
                {
                    ClearScreen();
                    winText.gameObject.SetActive(true);
                    Timerflow = false;
                    break;
                }
            case GameState.Lose:
                {
                    ClearScreen();
                    loseText.gameObject.SetActive(true);
                    Timerflow = false;
                    break;
                }
            default:
                break;
        }
    }
}
