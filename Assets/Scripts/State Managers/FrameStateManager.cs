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

	public virtual void Execute() {}
}
