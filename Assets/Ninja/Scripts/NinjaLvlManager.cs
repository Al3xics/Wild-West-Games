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
    [SerializeField] private int time = 8;
    [SerializeField] private int score = 0;
    [SerializeField] GameObject border;
    [SerializeField] GameObject depop;

    private Vector3[] corners;
    // Start is called before the first frame update
    void Start()
    {
        //if (GameManager.Instance.Difficulty > 0 && GameManager.Instance.Difficulty <= 20)
        //{
        //    lvl = 1;
        //}
        //else if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
        //{
        //    lvl = 2;
        //}
        //else if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
        //{
        //    lvl = 3;
        //}
        //else if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
        //{
        //    lvl = 4;
        //}
        //else if (GameManager.Instance.Difficulty > 80 && GameManager.Instance.Difficulty <= 100)
        //{
        //    lvl = 5;
        //}
        //else
        //{
        //    lvl = 1;
        //}

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

        StartCoroutine(Timing(time-lvl));
    }

    // Update is called once per frame
    void Update()
    {
        if (score>=1+lvl)
        {
            win = true;
            StopCoroutine(Timing(time));
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
                //GameManager.Instance.WinMiniGame();
                //Debug.Log("WIN");
            }
            else
            {
                //GameManager.Instance.EndMiniGame();
                //Debug.Log("NOPE");
            }
        }
    }

    private Vector3 CornersPosition()
    {
        // Obtenir les dimensions de la vue de la caméra en pixels
        float cameraWidthInPixels = Camera.main.pixelWidth;
        float cameraHeightInPixels = Camera.main.pixelHeight;

        // Calculer les coins du rectangle en fonction de la résolution de la caméra
        Vector2 topRight = new(cameraWidthInPixels / 2f, cameraHeightInPixels / 2f);
        Vector2 bottomLeft = new(-topRight.x, -topRight.y);

        corners = new Vector3[2];
        corners[0] = new Vector3(topRight.x, topRight.y, 0f);
        corners[1] = bottomLeft;

        //float randomX = Random.Range(bottomLeft.x, topRight.x);
        //float randomY = Random.Range(bottomLeft.y, topRight.y);

        //Vector3 randomPosition = new(randomX, randomY, 0f);

        return topRight;
    }

    private IEnumerator Timing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        end = true;
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
