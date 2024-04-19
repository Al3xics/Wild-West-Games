using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_Color : MonoBehaviour
{
    [SerializeField] Difference_LM Difference_LM_script;
    private TextMeshPro _textMeshPro;
    void Start()
    {
        _textMeshPro = this.gameObject.GetComponent<TextMeshPro>();

        int seconds = Mathf.FloorToInt(Difference_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Difference_LM_script.Mytimer - seconds) * 1000f);
        _textMeshPro.SetText(string.Format("{0:00}:{1:000}", seconds, milliseconds));
    }

    public void UpdateTimer()
    {
        int seconds = Mathf.FloorToInt(Difference_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Difference_LM_script.Mytimer - seconds) * 1000f);

        _textMeshPro.SetText(string.Format("{0:00}:{1:000}", seconds, milliseconds));
    }
}
