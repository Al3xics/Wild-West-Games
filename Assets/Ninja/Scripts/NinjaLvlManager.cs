using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NinjaLvlManager : MonoBehaviour
{
    [SerializeField] private bool end = false;
    [SerializeField] private bool win = false;
    [SerializeField] private float lvl;
    [SerializeField] private int time = 8;
    [SerializeField] private int score = 0;
    [SerializeField] GameObject borders;

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

        Instantiate(borders, CornersPosition(), gameObject.transform.rotation);

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
        // Obtenir les dimensions de la vue de la cam�ra en pixels
        float cameraWidthInPixels = Camera.main.pixelWidth;
        float cameraHeightInPixels = Camera.main.pixelHeight;

        // Calculer les coins du rectangle en fonction de la r�solution de la cam�ra
        Vector2 topRight = new(cameraWidthInPixels / 2f, cameraHeightInPixels / 2f);
        Vector2 bottomLeft = new(-topRight.x, -topRight.y);

        corners = new Vector3[2];
        corners[0] = new Vector3 (topRight.x, topRight.y, 0f);
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
