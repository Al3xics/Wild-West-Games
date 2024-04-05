using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    [SerializeField] Text NumberMin;
    [SerializeField] Text NumberMax;

    [SerializeField] int RandValueMin;
    [SerializeField] int RandValueMax;

    public int interval;
    public float number;
    public bool stopTouch = false;

    [HideInInspector] public int ValueMax;
    [HideInInspector] public int randomNumber;


    void Start()
    {
        randomNumber = Random.Range(RandValueMin, RandValueMax);
        NumberMin.text = randomNumber.ToString();
        
        ValueMax = randomNumber + interval;
        NumberMax.text = ValueMax.ToString();

    }

    private void Update()
    {
        if(number> ValueMax || number >= randomNumber && number <= ValueMax)
        {
            stopTouch = true;
        }

    }
}
