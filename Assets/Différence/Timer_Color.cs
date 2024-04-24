using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_Color : MonoBehaviour
{
    [SerializeField] Difference_LM Difference_LM_script;
    private TMP_Text _textMeshPro;
    void Start()
    {
        _textMeshPro = this.gameObject.GetComponent<TMP_Text>();

        int seconds = Mathf.FloorToInt(Difference_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Difference_LM_script.Mytimer - seconds) * 100f);
        _textMeshPro.text = (string.Format("{0:00}:{1:00}", seconds, milliseconds));
    }

    public void UpdateTimerColor()
    {
        int seconds = Mathf.FloorToInt(Difference_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Difference_LM_script.Mytimer - seconds) * 100f);

        _textMeshPro.text = (string.Format("{0:00}:{1:00}", seconds, milliseconds));
    }
}
