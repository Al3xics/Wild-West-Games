using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    [SerializeField] Text NumberMin;
    [SerializeField] Text NumberMax;

    [SerializeField] float RandValueMin;
    [SerializeField] float RandValueMax;
    [SerializeField] float newValueMax;

    [SerializeField] Image ImageMin;
    [SerializeField] Image ImageMax;

    public float interval;
    public float number;

    [HideInInspector] public float ValueMax;
    [HideInInspector] public float randomNumber;


    void Start()
    {
        randomNumber = Random.Range(RandValueMin, RandValueMax);
        NumberMin.text = randomNumber.ToString();
        
        ValueMax = randomNumber + interval;
        NumberMax.text = ValueMax.ToString();

        ImageMin.fillAmount = randomNumber / 100;

        newValueMax = 100 - ValueMax;
        ImageMax.fillAmount = newValueMax / 100;
    }

    private void Update()
    {

    }
}
