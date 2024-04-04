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
    [SerializeField] int interval;

    [HideInInspector] public int ValueMax;
    [HideInInspector] public int randomNumber;


    void Start()
    {
        randomNumber = Random.Range(RandValueMin, RandValueMax);
        NumberMin.text = randomNumber.ToString();
        
        ValueMax = randomNumber + interval;
        NumberMax.text = ValueMax.ToString();

    }
}
