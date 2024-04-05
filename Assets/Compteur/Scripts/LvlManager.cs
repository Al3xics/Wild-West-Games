using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] int lvl;
    [SerializeField] NumberManager numberManager;

    // Start is called before the first frame update
    void Start()
    {
        numberManager.interval = numberManager.interval - (lvl * 2);
    }

}
