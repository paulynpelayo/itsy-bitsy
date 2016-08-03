using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (SideScrollExample))]
[ExecuteInEditMode]
public class SideScrollExampleEditor : Editor {

    private SideScrollExample SSE;

    void OnEnable()
    {
        SSE = (SideScrollExample)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("EXECUTE"))
        {
            SSE.Start();
        }

        if (GUILayout.Button("CLEAR"))
        {
            foreach (Transform child in SSE.prefabParent.GetComponentsInChildren<Transform>())
            {
                if (child != SSE.prefabParent)
                    DestroyImmediate(child.gameObject);
            }
        }
    }
}
