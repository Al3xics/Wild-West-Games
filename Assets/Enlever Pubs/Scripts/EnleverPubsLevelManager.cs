using Nova;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnleverPubsLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject pubsPrefab;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerDuration = 10f;
    [SerializeField] private float timer;

    [SerializeField] private GameObject UIRoot;
    private List<GameObject> pubsList = new();
    private int difficultyLevel = 1;
    private int numberOfPubs = 1;
    private bool timerFlow = true;

    void Start()
    {
        pubsList.Clear();
        timer = timerDuration;
        //UIRoot = GameObject.Find("Canvas");

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
    private void OnDrawGizmos()
    {
        // Obtenir la cam�ra principale
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera found. Please ensure there is a camera tagged as 'MainCamera'.");
            return;
        }

        // Obtenir les dimensions de la vue de la cam�ra
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculer les coins de la zone de spawn
        Vector3 topLeft = mainCamera.rect.position + new Vector2(-cameraWidth / 2f, cameraHeight / 2f);
        Vector3 topRight = mainCamera.rect.position + new Vector2(cameraWidth / 2f, cameraHeight / 2f);
        Vector3 bottomLeft = mainCamera.rect.position + new Vector2(-cameraWidth / 2f, -cameraHeight / 2f);
        Vector3 bottomRight = mainCamera.rect.position + new Vector2(cameraWidth / 2f, -cameraHeight / 2f);

        // Dessiner les lignes repr�sentant la zone de spawn
        Gizmos.color = Color.green;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    private void CreatePubs()
    {
        if (pubsPrefab != null)
        {
            for (int i = 0; i < numberOfPubs; i++)
            {
                GameObject pubs = Instantiate(pubsPrefab, UIRoot.transform);
                Vector3 value = RandomSpawnPosition();
                pubs.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(value.x, value.y, Camera.main.nearClipPlane));
                //pubs.transform.position = RandomSpawnPosition();
                pubsList.Add(pubs);
            }
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        /*        // Obtenir les dimensions de la vue de la cam�ra en pixels
                float cameraWidthInPixels = Camera.main.pixelWidth;
                float cameraHeightInPixels = Camera.main.pixelHeight;

                // Calculer les coins du rectangle en fonction de la r�solution de la cam�ra
                //Vector2 topRight = new(cameraWidthInPixels / 3.5f, cameraHeightInPixels / 2.5f);
                Vector2 topRight = new(cameraWidthInPixels / 2.5f, cameraHeightInPixels / 3.5f);
                Vector2 bottomLeft = new(-topRight.x, -topRight.y);

                float randomX = Random.Range(bottomLeft.x, topRight.x);
                float randomY = Random.Range(bottomLeft.y, topRight.y);*/

        float randomX = Random.Range(0.2f, 0.8f);
        float randomY = Random.Range(0.2f, 0.8f);

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
            Debug.Log("WIN !!!");
            GameManager.Instance.WinMiniGame();
        }
    }

    private void CheckLose()
    {
        if (timer <= 0f)
        {
            timerFlow = false;
            Debug.Log("LOSE !!!");
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
