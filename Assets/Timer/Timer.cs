using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float Time;
    [SerializeField] private Slider slider;
    private bool cd = true;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Time;
        if (cd)
        {
            cd = false;
            StartCoroutine(Timing(1));
        }
    }

    public void SetValues(float max)
    {
        Debug.Log("yeah");
        slider.maxValue = max;
        Time = max;
    }

    private IEnumerator Timing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Time -= 1;
        cd = true;
    }
}
