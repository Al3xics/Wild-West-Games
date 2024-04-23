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
/*    private void OnDrawGizmos()
    {
        // Obtenir la caméra principale
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera found. Please ensure there is a camera tagged as 'MainCamera'.");
            return;
        }

        // Obtenir les dimensions de la vue de la caméra
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculer les coins de la zone de spawn
        Vector3 topLeft = mainCamera.rect.position + new Vector2(-cameraWidth / 2f, cameraHeight / 2f);
        Vector3 topRight = mainCamera.rect.position + new Vector2(cameraWidth / 2f, cameraHeight / 2f);
        Vector3 bottomLeft = mainCamera.rect.position + new Vector2(-cameraWidth / 2f, -cameraHeight / 2f);
        Vector3 bottomRight = mainCamera.rect.position + new Vector2(cameraWidth / 2f, -cameraHeight / 2f);

        // Dessiner les lignes représentant la zone de spawn
        Gizmos.color = Color.green;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }*/

    private void CreatePubs()
    {
        if (pubsPrefab != null)
        {
            for (int i = 0; i < numberOfPubs; i++)
            {
                //Vector3 pos = Vector3.zero;
                GameObject pubs = Instantiate(pubsPrefab);
                pubs.transform.SetParent(UIRoot.transform, false);
                RectTransform uiRectTransform = pubs.GetComponent<RectTransform>();
                /*                pubs.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                                Vector3 value = RandomSpawnPosition();
                                pubs.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(value.x, value.y, Camera.main.nearClipPlane));*/
                Vector3 value = RandomSpawnPosition();
                uiRectTransform.anchoredPosition = new Vector2(value.x, value.y);
                uiRectTransform.pivot = new Vector2(0.5f, 0.5f);
                //pubs.transform.TransformPoint(pubs.transform.localPosition);
                //pubs.transform.localPosition = new Vector3(pubs.transform.position.x, pubs.transform.position.y , 0);
                //pubs.transform.position = RandomSpawnPosition();
                pubsList.Add(pubs);
            }
        }
    }

    private Vector3 RandomSpawnPosition()
    {
/*               // Obtenir les dimensions de la vue de la caméra en pixels
                float cameraWidthInPixels = Camera.main.pixelWidth;
                float cameraHeightInPixels = Camera.main.pixelHeight;*/
        float panelWidth = UIRoot.GetComponent<RectTransform>().rect.width;
        float panelHeight = UIRoot.GetComponent<RectTransform>().rect.height;

                // Calculer les coins du rectangle en fonction de la résolution de la caméra
                Vector2 topRight = new(panelWidth / 3.8f, panelHeight / 2.8f);
                Vector2 bottomLeft = new(-topRight.x, -topRight.y);

                float randomX = Random.Range(bottomLeft.x, topRight.x);
                float randomY = Random.Range(bottomLeft.y, topRight.y);


        Debug.Log("width : " + panelWidth);
        Debug.Log("height : " + panelHeight);

/*        float randomX = Random.Range(0, panelWidth);
        float randomY = Random.Range(0, panelHeight);*/

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
