using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;

	// POLYGONS
	public List<GameObject> Polygons = new List<GameObject>();

	// DECORATIONS
	public List<GameObject> Decorations = new List<GameObject>();

	// OTHER
	public Color BackgroundColor;
	public Light Lighting;

	public virtual void Execute() 
	{
		stateMachine.Execute();
	}

	public void EnableShapes()
	{
		foreach (GameObject polygon in Polygons)
		{
			polygon.SetActive(true);
		}

		foreach (GameObject deco in Decorations)
		{
			deco.SetActive(true);
		}

		this.gameObject.SetActive(true);
	}

	public void EnableLight()
	{
		Lighting.gameObject.SetActive(true);
	}

	public void DisableShapes()
	{
		foreach (GameObject polygon in Polygons)
		{
			polygon.SetActive(false);
		}

		foreach (GameObject deco in Decorations)
		{
			deco.SetActive(false);
		}
	}

	public void DisableLight()
	{
		Lighting.gameObject.SetActive(false);
	}
}
