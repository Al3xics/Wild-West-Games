using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnleverPubsLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject pubsPrefab;

    private GameObject UIRoot;
    private UIBlock2D UIRootBlock2D;
    private BoxCollider2D colliderCamera;
    private int difficultyLevel = 1;
    private Vector2 minPosition;
    private Vector2 maxPosition;

    void Start()
    {
        UIRoot = GameObject.Find("UIRoot");
        UIRootBlock2D = UIRoot.GetComponent<UIBlock2D>();

        //float currentDifficultyLevel = GameManager.Instance.Difficulty / 10;
        //difficultyLevel = Mathf.RoundToInt(currentDifficultyLevel);
        //if (difficultyLevel < 1) difficultyLevel = 1;

        if (pubsPrefab != null)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject pubs = Instantiate(pubsPrefab, UIRoot.transform);
                pubs.GetComponent<UIBlock2D>().Position = RandomSpawnPosition();
            } 
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        Vector3 raw = UIRootBlock2D.Size.Raw;
        Vector2 topRight = new(raw.x / 3.2f, raw.y / 2.4f);
        Vector2 bottomLeft = new(-topRight.x, -topRight.y);

        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);

        Vector3 randomPosition = new(randomX, randomY, 0f);

        return randomPosition;
    }
}
