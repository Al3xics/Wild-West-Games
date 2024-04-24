using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_GameManager : MonoBehaviour
{
    static public BU_GameManager instance;
    [SerializeField] private Transform BallPlacement;
    [SerializeField] private Transform[] BallPlacements;
    [SerializeField] private Transform[] BinPlacement;
    //[SerializeField] private Transform[] FanPlacement;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BinPrefab;
    //[SerializeField] private GameObject FanPrefab;

    [SerializeField] private int ballCount = 5;
    [SerializeField] private float power = 1000;



    private GameObject ball;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool validStart = true;
    private bool validEnd = false;
    private bool CanThrow = false;

    private GameObject[] ballsIndicator;

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

        Gizmos.color = Color.blue;
        foreach (Transform t in BallPlacements)
        {
            Gizmos.DrawSphere(t.position, 0.05f);
        }


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
        ballsIndicator = new GameObject[BallPlacements.Length];
        for (int i = 0; i < BallPlacements.Length; i++)
        {
            ballsIndicator[i] =  Instantiate(BallPrefab, BallPlacements[i].position , Quaternion.identity);
            ballsIndicator[i].GetComponent<Rigidbody>().isKinematic = true;
        }

        int iiii = 0;
        switch (GameManager.Instance.Difficulty)
        {
            case < 40:
                iiii = 0;
                break;
            case < 70:
                iiii = 1;
                break;
            case < 100:
                iiii = 2;
                break;
        }

        Instantiate(BinPrefab, BinPlacement[iiii].position, Quaternion.identity);
        CanThrow = true;
    }

    IEnumerator win()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.WinMiniGame();
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

    public void BallInBin()
    {
        StartCoroutine(win());
    }

    public void BallMissed()
    {
        ballCount--;
        if (ballCount > 0)
        {
            //if (ballCount > 1)
            {
                ballsIndicator[ballCount - 1].SetActive(false);
            }
            ResetBall();
        }
        else
        {
            GameManager.Instance.EndMiniGame();
        }
    }
    private void ResetBall()
    {
        ball.transform.position = BallPlacement.position;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        CanThrow = true;
    }
    private void throwBall()
    {
        //debug log for valid start and end
        Debug.Log("validStart: " + validStart + " validEnd: " + validEnd);
        if (CanThrow && validStart && validEnd)
        {
            ball.GetComponent<Rigidbody>().AddForce((endPos - startPos) * power);
            CanThrow = false;
        }
        
    }
}
