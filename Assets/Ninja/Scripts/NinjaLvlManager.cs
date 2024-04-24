using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class NinjaLvlManager : MonoBehaviour
{
    [SerializeField] private bool end = false;
    [SerializeField] private bool win = false;
    [SerializeField] private float lvl;
    private int time = 8;
    [SerializeField] private int score = 0;
    [SerializeField] GameObject border;
    [SerializeField] GameObject depop;
    [SerializeField] private Timer timer;

    private Vector3[] corners;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.Difficulty > 0 && GameManager.Instance.Difficulty <= 20)
        {
            lvl = 1;
        }
        else if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        {
            lvl = 2;
        }
        else if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        {
            lvl = 3;
        }
        else if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        {
            lvl = 4;
        }
        else if (GameManager.Instance.Difficulty > 80 && GameManager.Instance.Difficulty <= 100)
        {
            lvl = 5;
        }
        else
        {
            lvl = 1;
        }

        GameObject borderL = Instantiate(border, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)), gameObject.transform.rotation);
        GameObject borderR = Instantiate(border, Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)), gameObject.transform.rotation);
        GameObject borderU = Instantiate(border, Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)), gameObject.transform.rotation);
        GameObject dep = Instantiate(depop, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.5f, Camera.main.nearClipPlane)), gameObject.transform.rotation);

        float scaleY = Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)), Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)));
        float scaleX = Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)), Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)));

        borderR.transform.localScale = new Vector3(1, -scaleY, 1);
        borderL.transform.localScale = new Vector3(-1, -scaleY, 1);
        borderU.transform.localScale = new Vector3(-scaleX, 1, 1);
        dep.transform.localScale = new Vector3(scaleX*3, 1, 1);

        timer.SetValues(time-lvl);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.GetValues()<=0)
        {
            end = true;
        }

        else if (score>=1+lvl && timer.GetValues()>0)
        {
            win = true;
            end = true;
        }

        if (end)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                Destroy(go);
            }
            if (win)
            {
                GameManager.Instance.WinMiniGame();
            }
            else
            {
                GameManager.Instance.EndMiniGame();
            }
        }
    }

    public void SetEnd(bool b)
    {
        end = b;
    }

    public void SetScore(int i)
    {
        score = i;
    }

    public int GetScore()
    {
        return score;
    }

    public float GetLvl()
    {
        return lvl; 
    }
}
