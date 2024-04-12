using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid_BallCounter : MonoBehaviour
{
    public int BallCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            BallCount++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            BallCount--;
        }
    }

}
