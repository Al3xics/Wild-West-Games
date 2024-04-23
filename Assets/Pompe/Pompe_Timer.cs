using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pompe_Timer : MonoBehaviour
{
    [SerializeField] Pompe_LM Pompe_LM_script;
    private TextMeshProUGUI _textMeshPro;
    void Start()
    {
        _textMeshPro = this.gameObject.GetComponent<TextMeshProUGUI>();

        int seconds = Mathf.FloorToInt(Pompe_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Pompe_LM_script.Mytimer - seconds) * 1000f);
        _textMeshPro.SetText(string.Format("{0:00}:{1:000}", seconds, milliseconds));
    }

    public void UpdateTimer()
    {
        int seconds = Mathf.FloorToInt(Pompe_LM_script.Mytimer % 60f);
        int milliseconds = Mathf.FloorToInt((Pompe_LM_script.Mytimer - seconds) * 1000f);

        _textMeshPro.SetText(string.Format("{0:00}:{1:000}", seconds, milliseconds));
    }
}
