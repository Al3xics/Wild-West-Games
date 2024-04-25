using System.Collections;
using UnityEngine;

public class MemoryLvlManager : MonoBehaviour
{
    public static MemoryLvlManager instance;

    [SerializeField] private int level = 1;
    [SerializeField] private int time = 10;
    [SerializeField] private int score = 0;

    private int rows, columns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameManager.Instance != null) // Vérifiez si GameManager.Instance est null
        {
            if (GameManager.Instance.Difficulty > 0 && GameManager.Instance.Difficulty <= 20)
                level = 1;
            else if (GameManager.Instance.Difficulty > 20 && GameManager.Instance.Difficulty <= 40)
                level = 2;
            else if (GameManager.Instance.Difficulty > 40 && GameManager.Instance.Difficulty <= 60)
                level = 3;
            else if (GameManager.Instance.Difficulty > 60 && GameManager.Instance.Difficulty <= 80)
                level = 4;
            else if (GameManager.Instance.Difficulty > 80 && GameManager.Instance.Difficulty <= 100)
                level = 5;
            else
                level = 1; // Niveau par défaut
        }
        SetDifficulty(level);
    }

    public void SetDifficulty(int dif)
    {
        switch (dif)
        {
            case 1:
                rows = 2;
                columns = 2;
                break;
            case 2:
                rows = 3;
                columns = 2;
                break;
            case 3:
                rows = 4;
                columns = 2;
                break;
            case 4:
                rows = 4;
                columns = 3;
                break;
            case 5:
                rows = 4;
                columns = 4;
                break;
            default:
                rows = 2;
                columns = 2;
                break;
        }
    }

    public int Rows
    {
       get { return rows; }
    }
    public int Columns 
    {
        get { return columns; } 
    }

    public void EndGame(bool fin)
    {
        if (fin)
        {
            GameManager.Instance.WinMiniGame();
            
        }
        else
        {
            GameManager.Instance.EndMiniGame();
        }
    }

    public void SetScore(int value)
    {
        score = value;
    }

    public int GetScore()
    {
       return score;
    }

    public int Level
    {
        get { return level; }
    }
}
