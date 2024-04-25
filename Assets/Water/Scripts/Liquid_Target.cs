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

    public bool doOnce = false;

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
        setupGame((int)GameManager.Instance.Difficulty);
        doOnce = false;
    }

    public void setupGame(int difficulty)
    {
        //difficulty go from 0 to 100;
        float sizeTarget = Mathf.Lerp(0.80f, 0.20f,difficulty/100f);
        float centerTarget = Random.Range((3f+ sizeTarget/2),(4.5f-sizeTarget/2));        

        float sizeAbovetarget = sizeTarget / 2;
        float centerAbovetarget = centerTarget + sizeTarget / 2 + sizeAbovetarget / 2;
        
        Target.transform.localScale = new Vector3(Target.transform.localScale.x, sizeTarget, Target.transform.localScale.y);
        Target.transform.localPosition = new Vector3(Target.transform.localPosition.x, centerTarget, Target.transform.localPosition.z);
        Abovetarget.transform.localScale = new Vector3(Abovetarget.transform.localScale.x, sizeAbovetarget, Abovetarget.transform.localScale.y);
        Abovetarget.transform.localPosition = new Vector3(Abovetarget.transform.localPosition.x, centerAbovetarget, Abovetarget.transform.localPosition.z);
    }


    // Update is called once per frame
    void Update()
    {

        if (timer <= 0 && !doOnce)
        {
            doOnce = true;
            StartCoroutine(endGame());

        }
        
        if (timer <= 0)
        {
            Vector3 z = Physics2D.gravity;
            Vector3 vec = new Vector3(0, -9, 8);
            Vector3 lerpedValue = Vector3.Lerp(z, vec, 0.2f);
            Physics2D.gravity = lerpedValue;
            
        }
        else
        {
            if (Mathf.Abs(cup.transform.rotation.z) <= 10 && BallCounterTarget.BallCount > 0 && BallCounterAbovetarget.BallCount == 0)
            {
                //GameManager.Instance.WinMiniGame();
            }
            timer -= Time.deltaTime;
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(4);
        if (BallCounterTarget.BallCount > 0 && BallCounterAbovetarget.BallCount == 0)
        {
            GameManager.Instance.WinMiniGame();
        }
        else
        {
            GameManager.Instance.EndMiniGame();
        }
    }

}
