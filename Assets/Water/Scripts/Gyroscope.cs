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

        //Physics.gravity = new Vector3(0, 12, 0);
    }

    void Update()
    {
        //Rot(monGyro.rotationRateUnbiased.z);
        Rot(monGyro.rotationRateUnbiased.z);

        //transform.Rotate(monGyro.rotationRateUnbiased * speedRotation);
    }

    public void Rot(float r) 
    {
        //transform.Rotate(0, 0, r * speedRotation);
        transform.Rotate(Vector3.forward, r * speedRotation);

        Physics2D.gravity = Quaternion.Euler(0, 0, r * -speedRotation) * Physics2D.gravity;

        //Physics.gravity = new Vector3(0,12,0);

        Debug.Log(r + " -> " + Physics2D.gravity + "  | " + monGyro.attitude.eulerAngles);
        

    }
}
