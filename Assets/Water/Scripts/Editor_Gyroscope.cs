using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(Gyroscope))] // Replace MyScript with the name of your script
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