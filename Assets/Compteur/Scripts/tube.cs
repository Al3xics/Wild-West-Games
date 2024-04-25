using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tube : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float maxValue;
    [SerializeField] compteurBtn compteurBtn;

    [SerializeField] private float power = 10;


    public float value;


    // Update is called once per frame
    void Update()
    {
        image.fillAmount = value / maxValue;

        if (value >= 100)
        {
            value = 100;
        }

        if (compteurBtn.isPressed)
        {
            value += power * Time.deltaTime;
        }
    }       
}
