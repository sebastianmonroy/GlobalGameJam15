using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawStateManager : FrameStateManager {

	SimpleState idleState, finishedState;	
	
	void Start() 
	{
		idleState = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "IDLE");
		finishedState = new SimpleState(null, null, null, "FINISHED");

		stateMachine.SwitchStates(idleState);
	}
	
	#region IDLE
	public GameObject Pad1, Pad2, Pad3, Pad4;
	public Color touchedColor;
	private Color startColor;
	private float progress1, progress2, progress3, progress4;

	void IdleEnter() 
	{
		startColor = Pad1.renderer.material.color;
	}
	
	void IdleUpdate() 
	{
		List<GameObject> pads = new List<GameObject>();
		foreach (Finger finger in GestureHandler.instance.fingers)
		{
			if (finger.hitObject == Pad1 && !pads.Contains(Pad1))
			{
				pads.Add(Pad1);
			}
			else if (finger.hitObject == Pad2 && !pads.Contains(Pad2))
			{
				pads.Add(Pad2);
			}
			else if (finger.hitObject == Pad3 && !pads.Contains(Pad3))
			{
				pads.Add(Pad3);
			}
			else if (finger.hitObject == Pad4 && !pads.Contains(Pad4))
			{
				pads.Add(Pad4);
			}
		}

		if (pads.Contains(Pad1))
		{
			progress1 = TouchedColor(Pad1, progress1);
		} 
		else
		{
			progress1 = ResetColor(Pad1, progress1);
		}

		if (pads.Contains(Pad2))
		{
			progress2 = TouchedColor(Pad2, progress2);
		} 
		else
		{
			progress2 = ResetColor(Pad2, progress2);
		}

		if (pads.Contains(Pad3))
		{
			progress3 = TouchedColor(Pad3, progress3);
		} 
		else
		{
			progress3 = ResetColor(Pad3, progress3);
		}

		if (pads.Contains(Pad4))
		{
			progress4 = TouchedColor(Pad4, progress4);
		} 
		else
		{
			progress4 = ResetColor(Pad4, progress4);
		}

		if (pads.Count > 3)
		{
			if (progress1 > 0.85f && progress2 > 0.85f && progress3 > 0.85f && progress4 > 0.85f)
			{
				stateMachine.SwitchStates(finishedState);
			}
		}
	}

		float TouchedColor(GameObject pad, float progress)
		{
			pad.renderer.material.color = Color.Lerp(pad.renderer.material.color, touchedColor, Time.deltaTime);
			progress = Mathf.Lerp(progress, 1f, Time.deltaTime);
			return progress;
		}

		float ResetColor(GameObject pad, float progress)
		{
			pad.renderer.material.color = Color.Lerp(pad.renderer.material.color, startColor, Time.deltaTime);
			progress = Mathf.Lerp(progress, 0f, Time.deltaTime);
			return progress;
		}
	
	void IdleExit() {}
	#endregion
	
}
