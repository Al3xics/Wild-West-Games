using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManagerJauge : MonoBehaviour
{
    [SerializeField] Slider sliderMin;
    [SerializeField] Slider sliderMax;

    [SerializeField] float RandValueMin;
    [SerializeField] float RandValueMax;
     
    public float interval;
    [HideInInspector] public float randNumber;
    [HideInInspector] public float valueMax;

    [SerializeField] private float time = 8;
    [SerializeField] private Timer tm;


    // Start is called before the first frame update
    void Start()
    {
        randNumber = Random.Range(RandValueMin, RandValueMax);
        sliderMin.value = randNumber;

        valueMax = randNumber + interval;
        float newValueMax = 1 - valueMax;
        sliderMax.value = newValueMax;

        tm.SetValues(time);
    }

    // Update is called once per frame
    void Update()
    {
        if (tm.GetValues() <= 0)
        {
            GameManager.Instance.EndMiniGame();
        }
    }
}
