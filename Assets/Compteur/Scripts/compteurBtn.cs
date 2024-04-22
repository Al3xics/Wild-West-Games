using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compteurBtn : MonoBehaviour
{
    [SerializeField] Text numberTxt;
    [SerializeField] NumberManager numberManager;
    [SerializeField] tube tube;
    
    [HideInInspector]  public bool isPressed = false; 

    private bool disabled = false;


    void Update()
    {
        numberManager.number = tube.value;

        numberTxt.text = numberManager.number.ToString();


        if(disabled)
        {
            if(numberManager.number >= numberManager.randomNumber && numberManager.number <= numberManager.ValueMax)
            {
                GameManager.Instance.WinMiniGame();
            }

            else
            {
                GameManager.Instance.EndMiniGame();
            }
        }
    }


    void OnMouseDown()
    {
        if (disabled == false)
        {
            isPressed = true;
        }

    }

 
    void OnMouseUp()
    {
        isPressed = false;
        disabled = true;

    }

}
