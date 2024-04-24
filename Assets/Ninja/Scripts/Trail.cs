using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
        }
        if (Input.touchCount < 1)
        {
            Destroy(gameObject);
        }
    }
}
