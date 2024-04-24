using Nova;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnleverPubsLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject pubs1;
    [SerializeField] private GameObject pubs2;
    [SerializeField] private GameObject pubs3;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerDuration = 10f;
    [SerializeField] private float timer;

    [SerializeField] private GameObject UIRoot;
    [SerializeField] private Timer tm;
    private List<GameObject> pubsList = new();
    private int difficultyLevel = 1;
    private int numberOfPubs = 1;
    private bool timerFlow = true;

    void Start()
    {
        pubsList.Clear();
        timer = timerDuration;
        tm.SetValues(timerDuration);

        float currentDifficultyLevel = GameManager.Instance.Difficulty / 2;
        difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        if (difficultyLevel < 1) difficultyLevel = 1;

        numberOfPubs *= difficultyLevel;
        CreatePubs();
    }

    void Update()
    {
        if (timerFlow)
        {
            timer -= Time.deltaTime;
            CheckLose();
            UpdateTimerUI();
        }
    }

    private void CreatePubs()
    {
        if (pubs1 != null && pubs2 != null && pubs3 != null)
        {
            for (int i = 0; i < numberOfPubs; i++)
            {
                int rand = Random.Range(0, 2);
                GameObject pubs;
                switch (rand)
                {
                    case 0:
                        pubs = Instantiate(pubs1);
                        break;
                    case 1:
                        pubs = Instantiate(pubs2);
                        break;
                    case 2:
                        pubs = Instantiate(pubs3);
                        break;
                    default:
                        pubs = Instantiate(pubs1);
                        break;
                }
                pubs.transform.SetParent(UIRoot.transform, false);
                RectTransform uiRectTransform = pubs.GetComponent<RectTransform>();
                Vector3 value = RandomSpawnPosition();
                uiRectTransform.anchoredPosition = new Vector2(value.x, value.y);
                uiRectTransform.pivot = new Vector2(0.5f, 0.5f);
                pubsList.Add(pubs);
            }
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        float panelWidth = UIRoot.GetComponent<RectTransform>().rect.width;
        float panelHeight = UIRoot.GetComponent<RectTransform>().rect.height;

        // Calculer les coins du rectangle en fonction de la résolution de la caméra
        Vector2 topRight = new(panelWidth / 3.8f, panelHeight / 2.8f);
        Vector2 bottomLeft = new(-topRight.x, -topRight.y);

        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);

        Debug.Log("width : " + panelWidth);
        Debug.Log("height : " + panelHeight);

        Vector3 randomPosition = new(randomX, randomY, 0f);

        return randomPosition;
    }

    public void RemovePubsFromList(GameObject pub)
    {
        pubsList.Remove(pub);
        CheckWin();
    }

    private void CheckWin()
    {
        if (pubsList.Count == 0 && timer > 0f)
        {
            timerFlow = false;
            GameManager.Instance.WinMiniGame();
        }
    }

    private void CheckLose()
    {
        if (timer <= 0f)
        {
            timerFlow = false;
            GameManager.Instance.EndMiniGame();
        }
    }

    private void UpdateTimerUI()
    {
        int seconds;
        int milliseconds;

        if (timer > 0f)
        {
            seconds = Mathf.FloorToInt(timer % 60f);
            milliseconds = Mathf.FloorToInt((timer - seconds) * 100f);
        }
        else
        {
            seconds = 0;
            milliseconds = 0;
        }

        timerText.text = string.Format("{0:0}:{1:00}", seconds, milliseconds);
    }
}
