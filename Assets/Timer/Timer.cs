using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float Time;
    [SerializeField] private Slider slider;
    private bool cd = true;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Time;
        if (cd)
        {
            cd = false;
            StartCoroutine(Timing(0.5f));
        }
    }

    public void SetValues(float max)
    {
        slider.maxValue = max;
        Time = max;
    }

    public float GetValues()
    {
        return Time;
    }

    private IEnumerator Timing(float waitTime)
    {
        Time -= 0.5f;
        yield return new WaitForSeconds(waitTime);
        cd = true;
    }
}
