using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(Gyroscope))] 
public class Editor_Gyroscope : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Access the target script
        Gyroscope myScript = (Gyroscope)target;

        if (GUILayout.RepeatButton("rotat left"))
        {
            myScript.Rot(10f);
        }
        if (GUILayout.RepeatButton("rotat right"))
        {
            myScript.Rot(-10f);
        }
    }
}

[CustomEditor(typeof(Liquid_Target))]
public class Editor_Liquid_Target : Editor
{
    int difficulty = 0;
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Access the target script
        Liquid_Target myScript = (Liquid_Target)target;

        difficulty = EditorGUILayout.IntSlider("Difficulty", difficulty, 0, 100);


        if (GUILayout.RepeatButton("generate random"))
        {
            myScript.setupGame(difficulty);
        }
    }
}