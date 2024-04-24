using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slash : MonoBehaviour
{
    private Vector3 lastPos;
    [SerializeField] private float forceToSlash;
    private RaycastHit2D hit;
    [SerializeField] GameObject trail;
    private bool trailOn = false;
    private Vector3 position;
    private float width;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            position = Camera.main.ScreenToWorldPoint(touch.position);
            if (Vector3.Distance(lastPos, position) > forceToSlash && lastPos != Vector3.zero)
            {
                Debug.Log("1");
                if (!trailOn)
                {
                    Debug.Log("2");
                    trailOn = true;
                    Instantiate(trail);
                }

                Debug.DrawLine(lastPos, position, Color.red, 1.0f, false);

                if (Physics2D.Linecast(lastPos, position))
                {
                    hit = Physics2D.Linecast(lastPos, position);

                    if (hit.collider.gameObject != null && hit.collider.gameObject.tag == "Fruit")
                    {
                        hit.collider.gameObject.GetComponent<Fruit>().Particles();
                        SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Cut);
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
                        {
                            go.GetComponent<NinjaLvlManager>().SetScore(go.GetComponent<NinjaLvlManager>().GetScore()+1);
                        }
                    }

                    if (hit.collider.gameObject != null && hit.collider.gameObject.tag == "Bomb")
                    {
                        hit.collider.gameObject.GetComponent<Fruit>().Particles();
                        SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Bomb);
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LvlManager"))
                        {
                            go.GetComponent<NinjaLvlManager>().SetEnd(true);
                        }
                    }
                }
            }
            lastPos = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            lastPos = Vector3.zero;
            trailOn = false;
        }
    }
}
