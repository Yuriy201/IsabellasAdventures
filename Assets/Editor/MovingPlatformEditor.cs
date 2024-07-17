using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MovingPlatform platform = (MovingPlatform)target;

        if (GUILayout.Button("Reset points positions"))
        {
            for (int i = 0; i < platform._movePoints.Length; i++)
            {
                platform._movePoints[i].point = platform.transform.position;
            }
        }
    }
}
