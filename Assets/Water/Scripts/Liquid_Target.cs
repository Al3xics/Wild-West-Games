using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid_Target : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Abovetarget;

    [SerializeField] private GameObject cup;

    private BoxCollider2D TargetCollider;
    private BoxCollider2D AbovetargetCollider;

    private Liquid_BallCounter BallCounterTarget;
    private Liquid_BallCounter BallCounterAbovetarget;

    private bool doOnce = false;

    [SerializeField] private float timer = 10;

    private void Awake()
    {
        TargetCollider = Target.GetComponent<BoxCollider2D>();
        AbovetargetCollider = Abovetarget.GetComponent<BoxCollider2D>();
        BallCounterTarget = Target.GetComponent<Liquid_BallCounter>();
        BallCounterAbovetarget = Abovetarget.GetComponent<Liquid_BallCounter>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Abovetarget.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && !doOnce)
            StartCoroutine(endGame());
        if (timer <= 0)
        {
            float z = cup.transform.rotation.eulerAngles.z;
            float lerpedValue = Mathf.Lerp(z, 0, 0.05f);
            cup.transform.rotation = Quaternion.Euler(0, 0, lerpedValue);
            
        }
        else
        {
            if (Mathf.Abs(cup.transform.rotation.z) <= 10 && BallCounterTarget.BallCount > 0 && BallCounterAbovetarget.BallCount == 0)
            {
                Debug.Log("you win");
            }
            timer -= Time.deltaTime;
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(2);
        if (BallCounterTarget.BallCount > 0 && BallCounterAbovetarget.BallCount == 0)
        {
            Debug.Log("you win");
        }
        else
        {
            Debug.Log("you lose");
        }
    }

}
