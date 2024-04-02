using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    [SerializeField] Text NumberMin;
    [SerializeField] Text NumberMax;

    [SerializeField] string NumberValueMin;
    [SerializeField] string NumberValueMax;


    void Update()
    {
        NumberMin.text = NumberValueMin.ToString();
        NumberMax.text = NumberValueMax.ToString();
    }
}
