using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrameStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;

	// POLYGONS
	public List<GameObject> Polygons = new List<GameObject>();

	// DECORATIONS
	public List<GameObject> Decorations = new List<GameObject>();

	public Color BackgroundColor;
	public Light Lighting;

	public virtual void Execute() 
	{
		stateMachine.Execute();
	}

	public void EnableAll()
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

	public void DisableAll()
	{
		foreach (GameObject polygon in Polygons)
		{
			polygon.SetActive(false);
		}

		foreach (GameObject deco in Decorations)
		{
			deco.SetActive(false);
		}

		Lighting.gameObject.SetActive(false);

		this.gameObject.SetActive(false);
	}
}
