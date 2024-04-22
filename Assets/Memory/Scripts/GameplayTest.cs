using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayTest : MonoBehaviour
{
    [SerializeField] private MemoryLvlManager memoryLvlManager;
    [SerializeField] private float timer = 30f;
    private bool gameIsRunning = true;
    public static GameplayTest instance;

    [SerializeField] List<Sprite> listItem = new List<Sprite>();
    [SerializeField] private List<string> listFound = new List<string>();
    [SerializeField] private GameObject Cube;
    public GameObject[,] blocs;
    private int returnCard = 0;
    private GameObject firstCard, secondCard;

    [SerializeField] private int rows = 2; // Nombre initial de lignes
    [SerializeField] private int columns = 2; // Nombre initial de colonnes

    private Camera mainCamera;
    private float cameraWidth;
    private float cameraHeight;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        memoryLvlManager = FindObjectOfType<MemoryLvlManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //listItem.AddRange(Resources.LoadAll<Sprite>("Sprites"));
        mainCamera = Camera.main;
        GetCameraSize();
        InitGame();

        /*int currentLevel = memoryLvlManager.Level;
        float difficulty = memoryLvlManager.DifficultyMultiplier;
        int pairs = memoryLvlManager.NumberOfPairsFound;*/
    }

    void GetCameraSize()
    {
        cameraHeight = 1.5f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect + 1f ;
    }

    void InitGame()
    {
        // Générer la grille de cartes en fonction de rows et columns
        blocs = new GameObject[rows, columns];
        GenerateGrid();
        Shuffle();
    }

    void GenerateGrid()
    {
        float offsetX = cameraWidth / columns;
        float offsetY = cameraHeight / rows;
        float startX = -cameraWidth / 2 + offsetX / 2;
        float startY = -cameraHeight / 2 + offsetY / 2;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newBlock = Instantiate(Cube); // Utilisez votre prefab ici
                newBlock.transform.position = new Vector3(startX + j * offsetX, startY + i * offsetY, 0);
                blocs[i, j] = newBlock;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame();
            }
            if (Input.GetMouseButtonDown(0)/*Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began*/)
            {
                if (returnCard == 2)
                {
                    returnCard = 0;

                    SpriteRenderer firstSprite = firstCard.GetComponentInChildren<SpriteRenderer>();
                    SpriteRenderer secondSprite = secondCard.GetComponentInChildren<SpriteRenderer>();

                    if (!listFound.Contains(firstSprite.sprite.name))
                    {
                        firstSprite.enabled = false;
                        secondSprite.enabled = false;
                    }

                }

                /*Touch touch = Input.GetTouch(0);*/

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition/*touch.position*/);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    SpriteRenderer spriteRenderer = hit.transform.Find("Sprite").GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = !spriteRenderer.enabled;

                    returnCard++;

                    if (returnCard == 1)
                    {
                        firstCard = hit.collider.gameObject;
                    }

                    if (returnCard == 2)
                    {
                        secondCard = hit.collider.gameObject;

                        if (firstCard.GetComponentInChildren<SpriteRenderer>().sprite.name == secondCard.GetComponentInChildren<SpriteRenderer>().sprite.name)
                            FoundPair(firstCard, secondCard);
                    }
                }
            }
        }
    }

    private void Shuffle()
    {
        int pairsNeeded = rows * columns / 2;
        List<Sprite> listTemp = new List<Sprite>(listItem);
        
        foreach (Sprite sprite in listItem)
        {
            listTemp.Add(sprite);
            listTemp.Add(sprite);
        }
        listTemp.RemoveRange(0, listItem.Count);

        if (pairsNeeded < listTemp.Count / 2)
            listTemp.RemoveRange(pairsNeeded * 2, listTemp.Count - pairsNeeded * 2);


        for (int i = 0; i < blocs.GetLength(0); i++)
        {
            for (int j = 0; j < blocs.GetLength(1); j++)
            {
                int rnd = Random.Range(0, listTemp.Count);
                SpriteRenderer target = blocs[i, j].GetComponentInChildren<SpriteRenderer>();
                target.sprite = listTemp[rnd];
                listTemp.RemoveAt(rnd);
                target.enabled = false;
            }
        }
    }

    private void FoundPair(GameObject obj1, GameObject obj2)
    {
        obj1.GetComponentInChildren<BoxCollider>().enabled = false;
        obj2.GetComponentInChildren<BoxCollider>().enabled = false;
        listFound.Add(obj1.GetComponentInChildren<SpriteRenderer>().sprite.name);
        listFound.Add(obj2.GetComponentInChildren<SpriteRenderer>().sprite.name);
        memoryLvlManager.PairFound();
    }

    void EndGame()
    {
        /*gameIsRunning = false;
        if (memoryLvlManager.NumberOfPairsFound == blocs.GetLength(0) * blocs.GetLength(1) / 2)
        {
            // Victoire
            GameManager.Instance.WinMiniGame();
        }
        else
        {
            // Défaite
            GameManager.Instance.EndMiniGame();
        }*/
    }
}