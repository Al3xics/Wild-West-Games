using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] float maxSpeed = 3;
    [SerializeField] float minSpeed = 0.5f;

    private bool direction = true;



    [HideInInspector] public float myVal = 0;


    // Update is called once per frame
    void FixedUpdate()
    {
        mySlider.value = myVal;

        float speed = Mathf.Lerp(minSpeed, maxSpeed, GameManager.Instance.Difficulty / 100);

        myVal += speed * Time.deltaTime * (direction ? 1 : -1);
        direction = direction ? myVal >= 1 ? false : true : myVal <= 0 ? true : false;
        //myVal = Mathf.Clamp01(mySlider.value);


        //if (canMove)
        //{
        //    canMove = false;

        //    if(GameManager.Instance.Difficulty <= 20)
        //    {
        //        StartCoroutine(addValue(0.01f, Time.deltaTime ));
        //    }

        //    if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        //    {
        //        StartCoroutine(addValue(0.007f, Time.deltaTime));
        //    }

        //    if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        //    {
        //        StartCoroutine(addValue(0.005f, Time.deltaTime));
        //    }

        //    if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        //    {
        //        StartCoroutine(addValue(0.003f, Time.deltaTime));
        //    }

        //    if (GameManager.Instance.Difficulty > 80)
        //    {
        //        StartCoroutine(addValue(0.001f, Time.deltaTime));
        //    }
        //}

    }

    //private IEnumerator addValue(float a, float delta) 
    //{

    //        if (minVal)
    //        {
    //            myVal += 0.03f * delta;

    //            if (myVal >= 1)
    //            {
    //                maxVal = true;
    //                minVal = false;
    //            }
    //        }

    //        if (maxVal)
    //        {
    //            myVal -= 0.03f * delta;

    //            if (myVal <= 0)
    //            {
    //                maxVal = false;
    //                minVal = true;
    //            }
    //        }


    //    yield return new WaitForSeconds(a);
    //    canMove = true;
    //}
}
