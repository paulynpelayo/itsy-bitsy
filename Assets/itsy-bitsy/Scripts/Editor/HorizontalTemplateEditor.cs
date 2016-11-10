using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (HorizontalTemplate))]
[ExecuteInEditMode]
public class HorizontalTemplateEditor : Editor {

	private HorizontalTemplate template;

    void OnEnable()
    {
		template = (HorizontalTemplate)target;
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
