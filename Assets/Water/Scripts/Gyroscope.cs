using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    [SerializeField, Range(0f, 0.5f)] private float speedRotation = 0.1f;
    [SerializeField, Range(0f, 10f)] private float boost =5f;
    [SerializeField] private Liquid_Target liquid_Target;
    UnityEngine.Gyroscope monGyro;

    private void Awake()
    {
        if (liquid_Target == null)
            liquid_Target = GetComponentInChildren<Liquid_Target>();
    }

    void Start()
    {
        StartCoroutine(InitializeGyro());
        //Physics.gravity = new Vector3(0, 12, 0);
    }

    IEnumerator InitializeGyro()
    {
        monGyro = Input.gyro;

        if (SystemInfo.supportsGyroscope)
        {
            monGyro.enabled = true;
        }

        yield return null;
    }

    void Update()
    {
        //Rot(monGyro.rotationRateUnbiased.z);
        if (!liquid_Target.doOnce)
            Rot(monGyro.rotationRateUnbiased.z * boost);


        //transform.Rotate(monGyro.rotationRateUnbiased * speedRotation);
    }

    public void Rot(float r) 
    {
        //transform.Rotate(0, 0, r * speedRotation);
        transform.Rotate(Vector3.forward, r * speedRotation);

        Physics2D.gravity = Quaternion.Euler(0, 0, -r * 3 ) * Physics2D.gravity ;
        //Physics.gravity = new Vector3(0,12,0);
    }
}
