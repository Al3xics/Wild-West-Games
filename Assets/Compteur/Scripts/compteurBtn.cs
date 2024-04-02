using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compteurBtn : MonoBehaviour
{
    [SerializeField] Text numberTxt;

    private int number = 0;
    private bool isPressed = false;
    private bool disabled = false;
    private bool timer = true;


    void Update()
    {
        numberTxt.text = number.ToString();

        if (isPressed)
        {
            if(timer)
            {
                timer = false;
                StartCoroutine(time(0.08f));
            }
        }
    }

    void OnMouseDown()
    {
        if(disabled == false)
        {
            isPressed = true;
        }  
    }

    void OnMouseUp()
    {
        isPressed = false;
        disabled = true;
    }

    private IEnumerator time(float sec)
    {
        number++;
        yield return new WaitForSeconds(sec);
        timer = true;
    }
}
