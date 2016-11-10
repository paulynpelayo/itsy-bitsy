using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (TopDownTemplate))]
[ExecuteInEditMode]
public class TopDownTemplateEditor : Editor {

	private TopDownTemplate template;

    void OnEnable()
    {
		template = (TopDownTemplate)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("GENERATE"))
        {
			template.Start();
        }

        if (GUILayout.Button("CLEAR"))
        {
			foreach (Transform child in template.prefabParent.GetComponentsInChildren<Transform>())
            {
				if (child != template.prefabParent)
                    DestroyImmediate(child.gameObject);
            }
        }
    }
}
