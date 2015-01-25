using UnityEngine;
using System.Collections;

public class BlendHandler: MonoBehaviour {
	public static BlendHandler Instance;
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

	public void Blend(Frame from, Frame to, float duration)
	{
		Timer timer = new Timer(duration);
		Blend(from, to, timer);
	}

	public void Blend(Frame from, Frame to, Timer timer)
	{
		blendDone = false;
		for (int i = 0; i < from.PolygonObjects.Count; i++)
		{
			StartCoroutine(BlendPolygonObject(from.PolygonObjects[i], to.PolygonObjects[i], timer));
		}

		for (int i = 0; i < from.DynamicDecorations.Count; i++)
		{
			StartCoroutine(FadeOutObject(from.DynamicDecorations[i], timer));
		}

		for (int i = 0; i < to.DynamicDecorations.Count; i++)
		{
			StartCoroutine(FadeInObject(to.DynamicDecorations[i], timer));
		}

		for (int i = 0; i < from.StaticDecorations.Count; i++)
		{
			StartCoroutine(FadeOutObject(from.StaticDecorations[i], timer));
		}

		for (int i = 0; i < to.StaticDecorations.Count; i++)
		{
			StartCoroutine(FadeInObject(to.StaticDecorations[i], timer));
		}

		StartCoroutine(BlendBackground(from.BackgroundColor, to.BackgroundColor, timer));
	}

	IEnumerator BlendBackground(Color startColor, Color endColor, Timer timer)
	{
		while (!timer.IsFinished())
		{
			Background.renderer.material.color = Color.Lerp(startColor, endColor, timer.Percent());
			yield return 0;
		}
	}

	IEnumerator FadeOutObject(GameObject fadeObject, Timer timer)
	{
		Color startColor = fadeObject.renderer.material.color;
		Color endColor = fadeObject.renderer.material.color;
		endColor.a = 0f;

		while (!timer.IsFinished())
		{
			fadeObject.renderer.material.color = Color.Lerp(startColor, endColor, timer.Percent());
			yield return 0;
		}
	}
	
	IEnumerator FadeInObject(GameObject fadeObject, Timer timer)
	{
		Color startColor = fadeObject.renderer.material.color;
		Color endColor = fadeObject.renderer.material.color;
		startColor.a = 0f;

		while (!timer.IsFinished())
		{
			fadeObject.renderer.material.color = Color.Lerp(startColor, endColor, timer.Percent());
			yield return 0;
		}
	}

	IEnumerator BlendPolygonObject(GameObject from, GameObject to, Timer timer)
	{
		Debug.Log("start coroutine");
		PolygonRenderer polygon1 = from.GetComponent<PolygonRenderer>();
		PolygonRenderer polygon2 = to.GetComponent<PolygonRenderer>();
		
		if (polygon1.Vertices.Length == polygon2.Vertices.Length)
		{
			int s = polygon1.Vertices.Length;
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
			
			while (!timer.IsFinished()) 
			{
				for (int i = 0; i < s; i++)
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
		} 
		else 
		{
			Debug.LogWarning("Meshes have different amount of vertices");
			yield return 0;
		}
		
		blendDone = true;
	}
}
