using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(GameManager))] // Replace MyScript with the name of your script
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Access the target script
        GameManager myScript = (GameManager)target;

        

        if (GUILayout.Button("New Mini Game"))
        {
            myScript.LoadNextMiniGame();
        }
        if (GUILayout.Button("Restart"))
        {
            myScript.RestartGame();
        }
        if (GUILayout.Button("Reset data"))
        {
            myScript.ResetAllData();
        }

    }
}