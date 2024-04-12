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

    public float slow;

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
            StartCoroutine(addValue(0.01f/GameManager.Instance.Difficulty));
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
