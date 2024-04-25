using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BU_GameManager : MonoBehaviour
{
    static public BU_GameManager instance;
    [SerializeField] private Transform BallPlacement;
    [SerializeField] private Transform[] BallPlacements;
    [SerializeField] private Transform BinPlacement;
    //[SerializeField] private Transform[] FanPlacement;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BinPrefab;
    //[SerializeField] private GameObject FanPrefab;

    [SerializeField] private int ballCount = 5;
    [SerializeField] private float power = 1000;
    [SerializeField] private float timer = 10;
    [SerializeField] private Timer tm;



    private GameObject ball;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool validStart = true;
    private bool CanThrow = false;
    private bool validEnd = false;

    private int missed;

    [SerializeField, Range(0, 5)] private float BinPostionVariableX = 3;
    [SerializeField, Range(0, 5)] private float BinPostionVariableZ = 2;


    private GameObject[] ballsIndicator;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(BinPlacement.position, 0.05f);
        //draw square for every point in front of bin placement that are +3 x max and +2 z max
        Gizmos.DrawLine(new Vector3(BinPlacement.position.x-BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z), new Vector3(BinPlacement.position.x + BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z));
        Gizmos.DrawLine(new Vector3(BinPlacement.position.x-BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z+ BinPostionVariableZ), new Vector3(BinPlacement.position.x + BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z+ BinPostionVariableZ));
        Gizmos.DrawLine(new Vector3(BinPlacement.position.x-BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z), new Vector3(BinPlacement.position.x- BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z+BinPostionVariableZ));
        Gizmos.DrawLine(new Vector3(BinPlacement.position.x+ BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z), new Vector3(BinPlacement.position.x+ BinPostionVariableX, BinPlacement.position.y, BinPlacement.position.z+BinPostionVariableZ));
        
        
        

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
            ballsIndicator[i] = Instantiate(BallPrefab, BallPlacements[i].position, Quaternion.identity);
            ballsIndicator[i].GetComponent<Rigidbody>().isKinematic = true;
        }


        float xx = UnityEngine.Random.Range(
            BinPlacement.position.x - Mathf.Lerp(0.0f, BinPostionVariableX, GameManager.Instance.Difficulty / 100),
            BinPlacement.position.x + Mathf.Lerp(0.0f, BinPostionVariableX, GameManager.Instance.Difficulty / 100)
            );

        float zz = UnityEngine.Random.Range(BinPlacement.position.z, BinPlacement.position.z + Mathf.Lerp(0.0f, BinPostionVariableZ, GameManager.Instance.Difficulty / 100));

        Instantiate(BinPrefab, new Vector3(xx, BinPlacement.position.y, zz), Quaternion.identity);

        CanThrow = true;
        missed = ballCount;

        tm.SetValues(timer);
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
            
            if (CanThrow)
                StartCoroutine(throwBall());

        }

        if (tm.GetValues() <= 0)
        {
            GameManager.Instance.EndMiniGame();
        }
    }

    public void BallInBin()
    {
        StartCoroutine(win());
    }

    public void BallMissed()
    {
        missed--;
        if (missed <= 0)
        {
            GameManager.Instance.EndMiniGame();            
        }
        else
        { 
            //ballsIndicator[ballCount - 1].SetActive(false);
            //ball.transform.position = BallPlacement.position;
        }
    }
    private IEnumerator throwBall()
    {
        ////debug log for valid start and end
        //Debug.Log("validStart: " + validStart + " validEnd: " + validEnd);
        //Debug.Log("ballCount: " + ballCount + " ballsIndicator.Length: " + ballsIndicator.Length);
        if (validStart && validEnd && ballCount >= 1  )
        {
            ball.GetComponent<Rigidbody>().AddForce((endPos - startPos) * power);
            CanThrow = false;
            yield return new WaitForSeconds(0.5f);
            ballCount--;
            if (ballCount >= 1)
            {
                ball = ballsIndicator[ballCount - 1];
                ball.transform.position = BallPlacement.position;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            CanThrow = true;
        }
    }
}
