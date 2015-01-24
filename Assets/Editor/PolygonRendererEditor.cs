using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PolygonRenderer))]
public class PolygonRendererEditor : Editor {

	int n;
	float height;

	public void SceneGUI(SceneView sceneView)
	{

	}

	public override void OnInspectorGUI (){
		base.OnInspectorGUI ();

		if (GestureHandler.instance != null)
		{
			List<Finger> fingers = GestureHandler.instance.fingers;
			foreach (Finger finger in fingers)
			{
				Debug.Log("finger");
				Vector2[] Vertices = (serializedObject.targetObject as PolygonRenderer).Vertices;
				for (int i = 0; i < Vertices.Length; i++)
				{
					Vector2 vertex = Vertices[i];
					if (Vector2.Distance(finger.GetWorldPosition(), vertex) < 0.5f)
					{
						(serializedObject.targetObject as PolygonRenderer).MoveVertex(i, finger.GetWorldPosition());
						EditorUtility.SetDirty(serializedObject.targetObject);
						Debug.Log("moved");
						break;
					}
				}
			}
		}

		// Update Polygon if a vertex is changed
		if ((serializedObject.targetObject as PolygonRenderer).VerticesChanged())
		{
			(serializedObject.targetObject as PolygonRenderer).Build();
		}

		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUIUtility.labelWidth);

		if (GUILayout.Button("Create N-Gon"))
		{
			Vector2[] verts = new Vector2[n];

			for (int i = 0; i < verts.Length; i++){
				verts[i] = new Vector2(Mathf.Sin(i * Mathf.PI * 2f / n), Mathf.Cos(i * Mathf.PI * 2f / n)) * height;
			}

			PolygonRenderer poly = (serializedObject.targetObject as PolygonRenderer);
			poly.Vertices = verts;
			poly.Build();
		}

		GUILayout.Space(10);
		GUILayout.BeginVertical();
		GUILayout.Label("N");
		GUILayout.Label("Radius");
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		n = EditorGUILayout.IntField(n);
		height = EditorGUILayout.FloatField(height);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		if (GUILayout.Button("Rebuild"))
		{
			(serializedObject.targetObject as PolygonRenderer).Build();
		}

		if (GUILayout.Button("Save Mesh"))
		{
			MeshFilter m = (serializedObject.targetObject as PolygonRenderer).GetComponent<MeshFilter>();
			AssetDatabase.CreateAsset(m.mesh, "Assets/Meshes/" + m.gameObject.name + " Mesh.asset");
		}
	}
}
