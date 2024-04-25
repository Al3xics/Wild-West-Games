using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compteurBtn : MonoBehaviour
{
    [SerializeField] NumberManager numberManager;
    [SerializeField] tube tube;
    
    [HideInInspector]  public bool isPressed = false; 

    private bool disabled = false;


    void Update()
    {
        if(isPressed)
        {
            transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
        }

        numberManager.number = tube.value;


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
            SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.WaterFill);
        }
    }

 
    void OnMouseUp()
    {
        isPressed = false;
        disabled = true;
        SFXManager.Instance.Audio.Stop();

    }

}
