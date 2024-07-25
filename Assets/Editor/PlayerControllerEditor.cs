using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]

public class PlayerControllerEditor : Editor
{

    private void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        PlayerController controller = (PlayerController)target;

        EditorGUILayout.LabelField("Max Health: " + controller.Stats.MaxHealth);
        EditorGUILayout.LabelField("Current Health: " + controller.Stats.CurrentHealth);

        base.OnInspectorGUI();
    }
}
