using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneByName : MonoBehaviour
{
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = GetComponentInChildren<TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnButtonClick()
    {
        Debug.Log("click");
        GameManager.Instance.isTraining = true;
        GameManager.Instance.miniGameTrain = sceneName;


        SceneManager.LoadScene(sceneName);
    }
}
