using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] private List<Sprite> listItem = new List<Sprite>();
    private List<string> listFound = new List<string>();
    [SerializeField] private GameObject Cube;
    private GameObject[,] blocs;
    private int returnCard = 0;
    private GameObject firstCard, secondCard;

    private Camera mainCamera;
    private float cameraWidth;
    private float cameraHeight;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null) // V�rifiez si mainCamera est null
        {
            GetCameraSize();
            InitGame();
        }
    }

    void GetCameraSize()
    {
        cameraHeight = 1.5f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect + 1f;
    }

    private void InitGame()
    {
        int rowsI = MemoryLvlManager.instance.Rows;
        int columnsI = MemoryLvlManager.instance.Columns;
        // G�n�rer la grille de cartes en fonction de rows et columns
        blocs = new GameObject[rowsI, columnsI];
        GenerateGrid();
        Shuffle();
    }

    private void GenerateGrid()
    {
        int rows = MemoryLvlManager.instance.Rows;
        int columns = MemoryLvlManager.instance.Columns;

        float offsetX = cameraWidth / columns;
        float offsetY = cameraHeight / rows;
        float startX = -cameraWidth / 2 + offsetX / 2;
        float startY = -cameraHeight / 2 + offsetY / 2 - .25f;
        

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newBlock = Instantiate(Cube); // Utilisez votre prefab ici 
                newBlock.transform.position = new Vector3(startX + j * offsetX, startY + i * offsetY + 0.2f, 0);
                blocs[i, j] = newBlock;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            

            //Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                SpriteRenderer spriteRenderer = hit.transform.Find("Sprite").GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = !spriteRenderer.enabled;

                returnCard++;

                if (returnCard == 1)
                {
                    firstCard = hit.collider.gameObject;
                    firstCard.GetComponent<Collider>().enabled = false;
                }

                if (returnCard == 2)
                {
                    secondCard = hit.collider.gameObject;
                    firstCard.GetComponent<Collider>().enabled = true;

                    SpriteRenderer firstSprite = firstCard.GetComponentInChildren<SpriteRenderer>();
                    SpriteRenderer secondSprite = secondCard.GetComponentInChildren<SpriteRenderer>();

                    if (firstCard.GetComponentInChildren<SpriteRenderer>().sprite.name == secondCard.GetComponentInChildren<SpriteRenderer>().sprite.name)
                    {
                        FoundPair(firstCard, secondCard);
                    }
                    else
                    {
                        StartCoroutine(DisableSpritesWithDelay(firstSprite, secondSprite));
                    }
                    returnCard = 0;

                    if (listFound.Count == MemoryLvlManager.instance.Rows * MemoryLvlManager.instance.Columns)
                    {
                        MemoryLvlManager.instance.EndGame(true);
                    }
                }
            }
        }

    }

    private void Shuffle()
    {
        int pairsNeeded = blocs.GetLength(0) * blocs.GetLength(1) / 2;
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
    }

    IEnumerator DisableSpritesWithDelay(SpriteRenderer firstSprite, SpriteRenderer secondSprite)
    {
        // Attendre pendant un court laps de temps avant de d�sactiver les sprites
        yield return new WaitForSeconds(.3f); // Vous pouvez ajuster ce d�lai selon vos besoins

        // D�sactiver les sprites apr�s le d�lai
        firstSprite.enabled = false;
        secondSprite.enabled = false;
    }
}