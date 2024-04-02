using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private GameObject[] bloc;
    // Start is called before the first frame update
    void Awake()
    {
        bloc = GameObject.FindGameObjectsWithTag("Bloc");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                SpriteRenderer spriteRenderer = hit.transform.Find("Sprite").GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }
}
