using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    [SerializeField] private List<Sprite> listItem = new List<Sprite>();
    [SerializeField] private List<string> listFound = new List<string>();
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
        if (mainCamera != null) // Vérifiez si mainCamera est null
        {
            InitGame();
        }
    }
    private void InitGame()
    {
        int rowsI = MemoryLvlManager.instance.Rows;
        int columnsI = MemoryLvlManager.instance.Columns;
        // Générer la grille de cartes en fonction de rows et columns
        blocs = new GameObject[rowsI, columnsI];
        GenerateGrid();
        Shuffle();
    }

    /*void GetCameraSize()
    {
        // Trouver la taille de l'écran
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // Convertir la taille de l'écran en coordonnées du monde
        Vector3 screenToWorld = mainCamera.ScreenToWorldPoint(screenSize);

        cameraHeight = Mathf.Abs(screenToWorld.x) * 2;
        cameraWidth = Mathf.Abs(screenToWorld.y) * 2;
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
    }*/
    private void GenerateGrid()
    {
        int rows = MemoryLvlManager.instance.Rows;
        int columns = MemoryLvlManager.instance.Columns;

        float blockSize = 1.0f; // Taille d'un bloc
        float padding = 0.5f; // Espacement entre les blocs

        GridLayoutGroup gridLayoutGroup = this.GetComponent<GridLayoutGroup>();

        // Calcul de l'espacement entre les blocs
        float totalPaddingX = (columns - 1) * padding;
        float totalPaddingY = (rows - 1) * padding;

        // Taille totale occupée par les blocs avec les espacements
        float totalBlockSizeX = columns * blockSize + totalPaddingX;
        float totalBlockSizeY = rows * blockSize + totalPaddingY;

        // Calcul du décalage initial en X et en Y
        float startX = -totalBlockSizeX / 2.0f + blockSize / 2.0f;
        float startY = -totalBlockSizeY / 2.0f + blockSize / 2.0f;

        // Placement des blocs
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float posX = startX + j * (blockSize + padding);
                float posY = startY + i * (blockSize + padding);
                GameObject newBlock = Instantiate(Cube);

                newBlock.transform.SetParent(gridLayoutGroup.transform, false);
                newBlock.transform.position = new Vector3(posX, posY, 0);
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
                        //SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Validation);
                    }
                    else
                    {
                        StartCoroutine(DisableSpritesWithDelay(firstSprite, secondSprite));
                        ///SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Fail);
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
        // Attendre pendant un court laps de temps avant de désactiver les sprites
        yield return new WaitForSeconds(.3f); // Vous pouvez ajuster ce délai selon vos besoins

        // Désactiver les sprites après le délai
        firstSprite.enabled = false;
        secondSprite.enabled = false;
    }
}