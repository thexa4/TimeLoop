using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameObjectCloner))]
public class GameObjectClonerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameObjectCloner cloner = (GameObjectCloner)target as GameObjectCloner;
        if (GUILayout.Button("Build Object") && cloner != null)
        {
            cloner.CloneObjects();
        }
    }
}