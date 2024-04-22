using UnityEngine;

public class MemoryLvlManager : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float difficultyMultiplier = 1.0f;
    private int numberOfPairsFound = 0;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public float DifficultyMultiplier
    {
        get { return difficultyMultiplier; }
        set { difficultyMultiplier = value; }
    }

    public int NumberOfPairsFound
    {
        get { return numberOfPairsFound; }
    }

    public void PairFound()
    {
        numberOfPairsFound++;
    }

    void Start()
    {
        int numberOfBlocks = GameplayTest.instance.blocs.GetLength(0) * GameplayTest.instance.blocs.GetLength(1);

        if (numberOfBlocks >= 4 && numberOfBlocks <= 16)
        {
            difficultyMultiplier = (numberOfBlocks - 4) / 2 + 1;
        }
    }

}
