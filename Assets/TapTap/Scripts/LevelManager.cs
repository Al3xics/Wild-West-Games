using Nova;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
   

    [SerializeField] float timeLimit = 11f;
    [SerializeField] float timer;
    [SerializeField] bool Timerflow = true;
    [SerializeField] Timer TimerVisual;

    [SerializeField] int difficultyLevel = 1;
    [SerializeField] bool TouchInput = true;
   
    private void Start()
    {
        //GameObject background = GameObject.Find("/Background");
        //if (background == null)
        //{
        //    return;
        //}
        //background.GetComponent<SpriteRenderer>().size = new Vector2(Screen.width, Screen.height);


        float currentDifficultyLevel = GameManager.Instance.Difficulty / 10;
        difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        if(difficultyLevel < 1) difficultyLevel = 1;

        if (difficultyLevel <= 4)
        {
            cockroachsAmount = 4 * difficultyLevel;
            cockroachsToWin = 1 * difficultyLevel;
        }
        else if (difficultyLevel <= 8)
        {
            cockroachsAmount = 20;
            cockroachsToWin = 4;
            timeLimit -= difficultyLevel - 3 ;
        }
        else
        {
            cockroachsAmount = 25;
            cockroachsToWin = 5;
            timeLimit = 4.0f;
        }

        
       
        

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
        TimerVisual.SetValues(timeLimit);
       
    }

    private void Update()
    {
        //Timer update
        if (Timerflow)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                    EndScreen(GameState.Lose);
                
                timer = 0;
            }
           

        }
        if (TouchInput)
        {
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

        }
        else
        {
            //Mouse Input
            if (Input.GetMouseButtonDown(0) && timer > 0)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D colliderHit = Physics2D.OverlapPoint(mousePosition);
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

        }

        //WinCheck
        if (cockroachsDead >= cockroachsToWin)
        {
            
                EndScreen(GameState.Win);
            
        }
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
                    GameManager.Instance.WinMiniGame();
                    
                    Timerflow = false;
                    break;
                }
            case GameState.Lose:
                {
                    GameManager.Instance.EndMiniGame();
                    
                    Timerflow = false;
                    break;
                }
            default:
                break;
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("TapTap");
    }
}
