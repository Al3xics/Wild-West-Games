using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoutonStop : MonoBehaviour
{
    [SerializeField] slider slider;
    [SerializeField] NumberManagerJauge numberManagerJauge;

    [SerializeField] GameObject poc;
    [SerializeField] GameObject ouch;
    [SerializeField] GameObject handle;
    [SerializeField] Sprite newSprite;

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
                poc.SetActive(true);
                SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Validation);
                GameManager.Instance.WinMiniGame();
            }

            else
            {
                ouch.SetActive(true);
                SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Fail);
                handle.GetComponent<Image>().sprite = newSprite;
                GameManager.Instance.EndMiniGame();
            }

        }
    }
    private void OnMouseDown()
    {
        slider.enabled = false;
    }
}
