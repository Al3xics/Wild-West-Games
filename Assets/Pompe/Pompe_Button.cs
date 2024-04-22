using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pompe_Button : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    private Material material;
    [SerializeField] private float value;
    [SerializeField] private Pompe_LM Pompe_LM_S;
    void Start()
    {
        material = targetRenderer.material;
        value = material.GetFloat("_Fill");
        value = 0;
        material.SetFloat("_Fill", value);
    }

    void Update()
    {
        
    }

    public void Pompe()
    {
        if (Pompe_LM_S.NbrClickedPompe < Pompe_LM_S.NumberOfClickPompe) 
        {
            Pompe_LM_S.NbrClickedPompe += 1;
            value += Pompe_LM_S.Diviseur;
            material.SetFloat("_Fill", value);
        }
    }

    public void Remplis()
    {
        if (Pompe_LM_S.NbrClickedRemplis < Pompe_LM_S.NumberOfClickRemplis)
        {
            Pompe_LM_S.NbrClickedRemplis += 1;
            value += Pompe_LM_S.Diviseur;
            material.SetFloat("_Fill", value);
        }
    }
}
