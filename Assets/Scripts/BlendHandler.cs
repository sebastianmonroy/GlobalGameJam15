using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlendHandler: MonoBehaviour {
	public static BlendHandler Instance;
	public GameObject EmptyPolygon;
	public GameObject Background;
	public float speedAlteration = 1f;
	public bool blendDone = true;

	void Start()
	{
		Instance = this;
	}

	public void Blend(GameObject _bFrom, GameObject _bTo, Timer timer)
	{
		blendDone = false;
		StartCoroutine(BlendPolygonObject(_bFrom, _bTo, timer));
	}
	
	public void Blend(GameObject _bFrom, GameObject _bTo, float duration)
	{
		blendDone = false;
		Timer timer = new Timer(duration);
		Blend(_bFrom, _bTo, timer);
	}

	public void Blend(FrameStateManager from, FrameStateManager to, float duration)
	{
		Timer timer = new Timer(duration);
		Blend(from, to, timer);
	}

	public void Blend(FrameStateManager from, FrameStateManager to, Timer timer)
	{
		Timer longTimer = new Timer(timer.Interval * 2f);
		blendDone = false;

		List<GameObject> fromPolys = from.PolygonObjects;
		List<GameObject> toPolys = to.PolygonObjects;

		while (from.PolygonObjects.Count < to.PolygonObjects.Count)
		{
			GameObject newPoly = Instantiate(EmptyPolygon, Vector3.zero, Quaternion.identity) as GameObject;
			newPoly.transform.parent = from.transform;
			fromPolys.Add(newPoly);
		}

		while (to.PolygonObjects.Count < from.PolygonObjects.Count)
		{
			GameObject newPoly = Instantiate(EmptyPolygon, Vector3.zero, Quaternion.identity) as GameObject;
			newPoly.transform.parent = to.transform;
			toPolys.Add(newPoly);
		}

		for (int i = 0; i < from.PolygonObjects.Count; i++)
		{
			StartCoroutine(BlendPolygonObject(from.PolygonObjects[i], to.PolygonObjects[i], timer));
		}
		
		for (int i = 0; i < from.DynamicDecorations.Count; i++)
		{
			StartCoroutine(FadeOutObject(from.DynamicDecorations[i], timer));
		}

		StartCoroutine(BlendDetails(from, to, timer));

		for (int i = 0; i < from.StaticDecorations.Count; i++)
		{
			StartCoroutine(FadeOutObject(from.StaticDecorations[i], timer));
		}

		for (int i = 0; i < to.StaticDecorations.Count; i++)
		{
			StartCoroutine(FadeInObject(to.StaticDecorations[i], timer));
		}

		StartCoroutine(BlendBackground(from.BackgroundColor, to.BackgroundColor, timer));
	
		StartCoroutine(BlendLighting(from.Lighting, to.Lighting, timer));
	}
	
	IEnumerator BlendDetails(FrameStateManager from, FrameStateManager to, Timer timer){
		while(timer.Percent() > 0.4f){
			yield return 0;
		}
		Timer timer2 = new Timer(timer.Interval/2);
		for (int i = 0; i < to.DynamicDecorations.Count; i++)
		{
			StartCoroutine(FadeInObject(to.DynamicDecorations[i], timer2));
		}
	}

	IEnumerator BlendBackground(Color startColor, Color endColor, Timer timer)
	{
		while (timer.Percent() < 1f) 
		{
			Background.renderer.material.color = Color.Lerp(startColor, endColor, timer.Percent());
			yield return 0;
		}
	}

	IEnumerator BlendLighting(Light startLight, Light endLight, Timer timer)
	{
		Color startColor = startLight.color;
		Color endColor = endLight.color;
		Vector3 startPosition = startLight.transform.position;
		Vector3 endPosition = endLight.transform.position;
		float startRange = startLight.range;
		float endRange = endLight.range;
		float startIntensity = startLight.intensity;
		float endIntensity = endLight.intensity;

		while (timer.Percent() < 1f) 
		{
			startLight.color = Color.Lerp(startColor, endColor, timer.Percent());
			startLight.transform.position = Vector3.Lerp(startPosition, endPosition, timer.Percent());
			startLight.range = Mathf.Lerp(startRange, endRange, timer.Percent());
			startLight.intensity = Mathf.Lerp(startIntensity, endIntensity, timer.Percent());
			yield return 0;
		}

		startLight.gameObject.SetActive(false);
		endLight.gameObject.SetActive(true);
	}

	IEnumerator FadeOutObject(GameObject fadeObject, Timer timer)
	{
		Vector3 startSize = fadeObject.transform.localScale;
		Vector3 endSize = Vector3.zero;

		while (timer.Percent() < 1f) 
		{
			fadeObject.transform.localScale = Vector3.Lerp(startSize, endSize, timer.Percent());
			yield return 0;
		}

		fadeObject.SetActive(false);
	}
	
	IEnumerator FadeInObject(GameObject fadeObject, Timer timer)
	{
		//GameObject newObject = Instantiate(fadeObject, fadeObject.transform.position, fadeObject.transform.rotation) as GameObject;
		//newObject.transform.localScale = Vector3.zero;
		fadeObject.SetActive(true);
		Vector3 startSize = Vector3.zero;
		Vector3 endSize = fadeObject.transform.localScale;

		while (timer.Percent() < 1f) 
		{
			fadeObject.transform.localScale = Vector3.Lerp(startSize, endSize, timer.Percent());
			yield return 0;
		}
	}

	IEnumerator BlendPolygonObject(GameObject from, GameObject to, Timer timer)
	{
		PolygonRenderer polygon1 = from.GetComponent<PolygonRenderer>();
		PolygonRenderer polygon2 = to.GetComponent<PolygonRenderer>();
		
		//if (polygon1.Vertices.Length == polygon2.Vertices.Length)
		//{
		Debug.Log("blend start");
		Vector2[] oldVertices = polygon1.Vertices;
		Quaternion oldRotation = from.transform.rotation;
		Vector3 oldScale = from.transform.localScale;
		Color oldColor = from.renderer.material.color;
		Vector3 oldPosition = from.transform.position;
		float oldThick = polygon1.Thickness;

		Vector2[] newVertices = polygon2.Vertices;
		Quaternion newRotation = to.transform.rotation;
		Vector3 newScale = to.transform.localScale;
		Color newColor = to.renderer.material.color;
		Vector3 newPosition = to.transform.position;
		float newThick = polygon2.Thickness;
		
		while (timer.Percent() < 1f) 
		{
			for (int i = 0; i < polygon1.Vertices.Length; i++)
			{
				//if(i == 0) Debug.Log("moving from " + polygon1.Vertices[i] + " to " + polygon2.Vertices[i]);
				polygon1.Vertices[i] = Vector2.Lerp(oldVertices[i], newVertices[i], timer.Percent());
				//color, rotation, thickness, scale
				from.renderer.material.color = Color.Lerp(oldColor, newColor, timer.Percent());
				from.transform.rotation = Quaternion.Lerp(oldRotation, newRotation, timer.Percent());
				from.transform.localScale = Vector3.Lerp(oldScale, newScale, timer.Percent());
				from.transform.position = Vector3.Lerp(oldPosition, newPosition, timer.Percent());
				polygon1.Thickness = Mathf.Lerp(oldThick, newThick, timer.Percent());
				//if(i == 0) Debug.Log("At position: " + polygon1.Vertices[i]);
			}
			polygon1.Modify();
			yield return 0;
		}

		Debug.Log("blend done");
		to.SetActive(true);	
		from.SetActive(false);
		blendDone = true;
		//} 
		//else 
		//{
		//	Debug.LogWarning("Meshes have different amount of vertices");
		//	yield return 0;
		//}
	}
}
