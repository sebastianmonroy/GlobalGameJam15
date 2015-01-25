using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrameStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;

	// POLYGONS
	public List<GameObject> PolygonObjects = new List<GameObject>();

	// DECORATIONS
	public List<GameObject> DynamicDecorations = new List<GameObject>();
	public List<GameObject> StaticDecorations = new List<GameObject>();
	public Color BackgroundColor;
	public Light Lighting;

	public virtual void Execute() 
	{
		stateMachine.Execute();
	}

	public void EnableAll()
	{
		foreach (GameObject polygon in PolygonObjects)
		{
			polygon.SetActive(true);
		}

		foreach (GameObject deco in DynamicDecorations)
		{
			deco.SetActive(true);
		}

		foreach (GameObject deco in StaticDecorations)
		{
			deco.SetActive(true);
		}
	}

	public void DisableAll()
	{
		foreach (GameObject polygon in PolygonObjects)
		{
			polygon.SetActive(false);
		}

		foreach (GameObject deco in DynamicDecorations)
		{
			deco.SetActive(false);
		}

		foreach (GameObject deco in StaticDecorations)
		{
			deco.SetActive(false);
		}
	}
}
