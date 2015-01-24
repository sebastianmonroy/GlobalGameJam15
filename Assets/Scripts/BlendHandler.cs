using UnityEngine;
using System.Collections;

public class BlendHandler : MonoBehaviour {
	//debug
	public GameObject g1, g2;
	void Start(){
		Blend(g1, g2);
	}
	//end debug
	
	public void Blend(GameObject _bFrom, GameObject _bTo){
		StartCoroutine(BlendRoutine(_bFrom, _bTo));
	}
	
	IEnumerator BlendRoutine(GameObject from, GameObject to){
		Debug.Log("start coroutine");
		PolygonRenderer verts1 = from.GetComponent<PolygonRenderer>();
		PolygonRenderer verts2 = to.GetComponent<PolygonRenderer>();
		
		if(verts1.Vertices.Length == verts2.Vertices.Length){
			int s = verts1.Vertices.Length;
			Vector2[] oldStart = verts1.Vertices;
			float startTime = Time.time;
			while(verts1.Vertices[s-1] != verts2.Vertices[s-1]) {
				for(int i = 0; i < s; i++){
					if(i == 0) Debug.Log("moving from " + verts1.Vertices[i] + " to " + verts2.Vertices[i]);
					verts1.Vertices[i] = Vector2.Lerp(oldStart[i], verts2.Vertices[i], Time.time - startTime);
					if(i == 0) Debug.Log("At position: " + verts1.Vertices[i]);
				}
				verts1.Build();
				yield return new WaitForSeconds(0.1f);
			}
		} else {
			Debug.LogWarning("Meshes have different amount of vertices");
			yield return new WaitForSeconds(0.001f);
		}
	}
}
