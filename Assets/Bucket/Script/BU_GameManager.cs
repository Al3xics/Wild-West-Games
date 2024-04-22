using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_GameManager : MonoBehaviour
{
    static public BU_GameManager instance;
    [SerializeField] private Transform BallPlacement;
    [SerializeField] private Transform[] BinPlacement;
    //[SerializeField] private Transform[] FanPlacement;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BinPrefab;
    //[SerializeField] private GameObject FanPrefab;

    [SerializeField] private int ballCount = 5;
    [SerializeField] private float power = 1000;



    [SerializeField] private GameObject ball;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool validStart = true;
    private bool validEnd = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Transform t in BinPlacement)
        {
            Gizmos.DrawSphere(t.position, 0.05f);
        }
        Gizmos.color = Color.white;
        //foreach (Transform t in FanPlacement)
        //{
        //    Gizmos.DrawSphere(t.position, 0.05f);
        //}

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(BallPlacement.position, 0.05f);


        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(startPos, 0.05f);
        Gizmos.DrawSphere(endPos, 0.05f);
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        ball = Instantiate(BallPrefab, BallPlacement.position, Quaternion.identity);

        Instantiate(BinPrefab, BinPlacement[Random.Range(0,BinPlacement.Length)].position, Quaternion.identity);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 1;
            startPos = Camera.main.ScreenToWorldPoint(pos);

            //validStart = Vector3.Distance(startPos, BallPlacement.position) < 0.1 ;
            validStart = true;

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 1.1f;
            endPos = Camera.main.ScreenToWorldPoint(pos);
            endPos.x = endPos.x / 2;
            validEnd = (endPos - startPos).y > 0;
            
            throwBall();

        }
    }

    public void BallMissed()
    {
        ballCount--;
        if (ballCount > 0)
        {
            ResetBall();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
    private void ResetBall()
    {
        ball.transform.position = BallPlacement.position;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    private void throwBall()
    {
        //debug log for valid start and end
        Debug.Log("validStart: " + validStart + " validEnd: " + validEnd);
        if (validStart && validEnd)
            ball.GetComponent<Rigidbody>().AddForce((endPos - startPos) * power);
        
    }
}
