using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonStop : MonoBehaviour
{
    [SerializeField] slider slider;
    [SerializeField] NumberManagerJauge numberManagerJauge;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!slider.enabled)
        {
            if(slider.myVal >= numberManagerJauge.randNumber && slider.myVal <= numberManagerJauge.valueMax)
            {
                GameManager.Instance.WinMiniGame();
            }

            else
            {
                GameManager.Instance.EndMiniGame();
            }

        }
    }

    private void OnMouseDown()
    {
        slider.enabled = false;
    }
}
