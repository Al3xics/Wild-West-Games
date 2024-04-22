using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    [SerializeField, Range(0f, 0.5f)] private float speedRotation = 0.1f;
    UnityEngine.Gyroscope monGyro;

    void Start()
    {
        monGyro = Input.gyro;

        if (SystemInfo.supportsGyroscope )
        {
            Debug.Log("Gyro ON");
            monGyro.enabled = true;
        }
        else
        {
            Debug.Log("Gyro OFF");
        }
    }

    void Update()
    {
        Rot(monGyro.rotationRateUnbiased.z);
    }

    public void Rot(float r) 
    { 
        transform.Rotate(0, 0, r * speedRotation);
    }
}
