using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BU_GameManager))] // Replace MyScript with the name of your script
public class Editor_Bucket : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        // Access the target script
        BU_GameManager myScript = (BU_GameManager)target;

        
    }
}