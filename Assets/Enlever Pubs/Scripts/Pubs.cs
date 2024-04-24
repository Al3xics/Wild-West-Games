using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pubs : MonoBehaviour
{
    private EnleverPubsLevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnleverPubsLevelManager>();
    }

    public void DestroyPubs()
    {
        Debug.Log("je suis cliqu�");
        levelManager.RemovePubsFromList(gameObject);
        Destroy(gameObject);
    }
}
