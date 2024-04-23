using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] List<Sprite> listItem = new List<Sprite>();
    private GameObject[] bloc;
    private int returnCard = 0;
    private GameObject firstCard, secondCard;
    [SerializeField]private List<string> listFound = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        bloc = GameObject.FindGameObjectsWithTag("Bloc");
    }

    private void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (returnCard == 2)
            {
                returnCard = 0;
                
                SpriteRenderer firstSprite = firstCard.GetComponentInChildren<SpriteRenderer>();
                SpriteRenderer secondSprite = secondCard.GetComponentInChildren<SpriteRenderer>();

                if(!listFound.Contains(firstSprite.sprite.name))
                {
                    firstSprite.enabled = false;
                    secondSprite.enabled = false;
                }

            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           
            if(Physics.Raycast(ray, out hit))
            {
                SpriteRenderer spriteRenderer = hit.transform.Find("Sprite").GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = !spriteRenderer.enabled;

                returnCard++;

                if(returnCard == 1)
                {
                    firstCard = hit.collider.gameObject;
                }

                if(returnCard == 2)
                {
                    secondCard = hit.collider.gameObject;

                    if (firstCard.GetComponentInChildren<SpriteRenderer>().sprite.name == secondCard.GetComponentInChildren<SpriteRenderer>().sprite.name)
                        FoundPair(firstCard, secondCard);
                }
            }
        }
    }

    private void Shuffle()
    {
        List<Sprite> listTemp = new List<Sprite>(listItem);
        listTemp.AddRange(listItem);

        for (int i = 0; i < bloc.Length; i++)
        {
            int rnd = Random.Range(0, listTemp.Count);
            SpriteRenderer target = bloc[i].transform.Find("Sprite").GetComponent<SpriteRenderer>();
            target.sprite = listTemp[rnd];
            listTemp.RemoveAt(rnd);
            target.enabled = false;
        }
    }

    private void FoundPair(GameObject obj1, GameObject obj2)
    {
        obj1.GetComponentInChildren<BoxCollider>().enabled = false;
        obj2.GetComponentInChildren<BoxCollider>().enabled = false; 
        listFound.Add(obj1.GetComponentInChildren<SpriteRenderer>().sprite.name);
    }

}
