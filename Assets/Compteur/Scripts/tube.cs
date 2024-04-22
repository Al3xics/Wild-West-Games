using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tube : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float maxValue;
    [SerializeField] compteurBtn compteurBtn;

    private bool canAdd = true;

    public float value;


    // Update is called once per frame
    void Update()
    {
        image.fillAmount = value / maxValue;

        if(value>= 100)
        {
            value = 100;
        }

        if(compteurBtn.isPressed)
        {
            if (canAdd)
            {
                canAdd = false;
                StartCoroutine(addFillAmount(0.01f));
            }
        }
       
    }

    private IEnumerator addFillAmount(float add)
    {
        value++;
        yield return new WaitForSeconds(add);
        canAdd = true;
    }
}
