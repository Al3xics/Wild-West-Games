using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Function_Difference : MonoBehaviour
{
    [SerializeField] private bool isWinning_Button;

    public bool IsWinning_Button
    {
        get { return isWinning_Button; }
        set { isWinning_Button = value; }
    }

    //[SerializeField] private bool isWinning_Button;
    private GameObject obj;
    void Start()
    {
        obj = GameObject.Find("LevelManager");
    }

    void Update()
    {
        
    }

/*    public void set_isWinningButton()
    {
        isWinning_Button = obj.GetComponent<Instanciate_Button>().Iswinner_Button;
    }*/

    public void EndMiniGame()
    {
        if (isWinning_Button)
        {
            SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Validation);
            GameManager.Instance.WinMiniGame();
            Debug.Log("win");
        }
        else
        {
            SFXManager.Instance.Audio.PlayOneShot(SFXManager.Instance.Fail);
            GameManager.Instance.EndMiniGame();
            Debug.Log("loose");
        }
    }
}
