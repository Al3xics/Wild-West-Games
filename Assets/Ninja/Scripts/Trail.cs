using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trail : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(touch.position)+new Vector3 (0,0,0.5f);
        }
        if (Input.touchCount < 1)
        {
            Destroy(gameObject);
        }
    }
}
