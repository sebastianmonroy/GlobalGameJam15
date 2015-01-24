using UnityEngine;
using System.Collections;

public class BlendHandler: MonoBehaviour {
	public static BlendHandler Instance;
	public float speedAlteration = 1f;
	public bool blendDone = true;

	void Start(){
		Instance = this;
	}

	public void Blend(GameObject _bFrom, GameObject _bTo, Timer timer){
		blendDone = false;
		StartCoroutine(BlendRoutine(_bFrom, _bTo, timer));
	}
	
	public void Blend(GameObject _bFrom, GameObject _bTo, float duration){
		blendDone = false;
		Timer timer = new Timer(duration);
		StartCoroutine(BlendRoutine(_bFrom, _bTo, timer));
	}
	
	IEnumerator BlendRoutine(GameObject from, GameObject to, Timer timer){
		Debug.Log("start coroutine");
		PolygonRenderer verts1 = from.GetComponent<PolygonRenderer>();
		PolygonRenderer verts2 = to.GetComponent<PolygonRenderer>();
		
		if(verts1.Vertices.Length == verts2.Vertices.Length){
			int s = verts1.Vertices.Length;
			Vector2[] oldStart = verts1.Vertices;
			Quaternion oldRot = from.transform.rotation;
			Vector3 oldScale = from.transform.localScale;
			Color oldColor = from.renderer.material.color;
			Vector3 oldTrans = from.transform.position;
			float oldThick = verts1.Thickness;
			
			while(!timer.IsFinished()) {
				for(int i = 0; i < s; i++){
					//if(i == 0) Debug.Log("moving from " + verts1.Vertices[i] + " to " + verts2.Vertices[i]);
					verts1.Vertices[i] = Vector2.Lerp(oldStart[i], verts2.Vertices[i], timer.Percent());
					//color, rotation, thickness, scale
					from.renderer.material.color = Color.Lerp(oldColor, to.renderer.material.color, timer.Percent());
					from.transform.rotation = Quaternion.Lerp(oldRot, to.transform.rotation, timer.Percent());
					from.transform.localScale = Vector3.Lerp(oldScale, to.transform.localScale, timer.Percent());
					from.transform.position = Vector3.Lerp(oldTrans, to.transform.position, timer.Percent());
					verts1.Thickness = Mathf.Lerp(oldThick, verts2.Thickness, timer.Percent());
					//if(i == 0) Debug.Log("At position: " + verts1.Vertices[i]);
				}
				verts1.Build();
				yield return new WaitForSeconds(0.1f);
			}
		} else {
			Debug.LogWarning("Meshes have different amount of vertices");
			yield return new WaitForSeconds(0.001f);
		}
		
		blendDone = true;
	}
}
