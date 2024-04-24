using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_PanierDetectior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            BU_GameManager.instance.BallInBin();
    }

}
