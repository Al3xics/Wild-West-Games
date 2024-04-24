using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Floor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(MissedShot());
        }
    }

    IEnumerator MissedShot()
    {
        yield return new WaitForSeconds(2);
        BU_GameManager.instance.BallMissed();
    }
}
