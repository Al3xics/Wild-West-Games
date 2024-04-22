using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField] Slider mySlider;

    private bool canMove = true;
    private bool maxVal = false;
    private bool minVal = true;

    [HideInInspector] public float myVal = 0;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        mySlider.value = myVal;

        if(myVal >= 1)
        {
            myVal = 1;
        }

        if (myVal <= 0)
        {
            myVal = 0;
        }

        if (canMove)
        {
            canMove = false;

            if(GameManager.Instance.Difficulty <= 20)
            {
                StartCoroutine(addValue(0.01f));
            }

            if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
            {
                StartCoroutine(addValue(0.007f));
            }

            if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
            {
                StartCoroutine(addValue(0.005f));
            }

            if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
            {
                StartCoroutine(addValue(0.003f));
            }

            if (GameManager.Instance.Difficulty > 80)
            {
                StartCoroutine(addValue(0.001f));
            }
        }

    }

    private IEnumerator addValue(float a) 
    {

            if (minVal)
            {
                myVal += 0.01f;

                if (myVal >= 1)
                {
                    maxVal = true;
                    minVal = false;
                }
            }

            if (maxVal)
            {
                myVal -= 0.01f;

                if (myVal <= 0)
                {
                    maxVal = false;
                    minVal = true;
                }
            }


        yield return new WaitForSeconds(a);
        canMove = true;
    }
}
