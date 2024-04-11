using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManagerJauge : MonoBehaviour
{
    [SerializeField] float lvl;
    [SerializeField] NumberManagerJauge managerJauge;


    // Start is called before the first frame update
    void Start()
    {
        managerJauge.interval = managerJauge.interval - ((lvl / 100) * 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
