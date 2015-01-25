using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BicycleStateManager : FrameStateManager
{
	private float rVel;
	private Vector2 oldOldMousePos;
	private Vector2 oldMousePos;
	private Vector2 mousePos;
	
	public GameObject wheel1;
	public GameObject wheel2;
	
	float aVel = 0;
	
	float aTotal = 0;
	
	
	
	SimpleState bicycleState, finishedState;
	
	void Start() {
		bicycleState = new SimpleState(bicycleEnter, bicycleUpdate, bicycleExit, "BICYCLE");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(bicycleState);
		
		oldOldMousePos = Vector2.zero;
		oldMousePos = Vector2.zero;
		mousePos = Vector2.zero;
	}
	
	void bicycleUpdate() {
		//	Debug.Log ("here");
		Turn ();
	}
	
	void Turn ()
	{
		if (GestureHandler.instance.fingers.Count > 0) {
			
			oldOldMousePos = oldMousePos;
			oldMousePos = mousePos;
			mousePos = GestureHandler.instance.fingers[0].GetWorldPosition();
			
			Vector2 oldOldToOld = new Vector2 (oldMousePos.x - oldOldMousePos.x, oldMousePos.y - oldOldMousePos.y);
			Vector2 oldOldToCurr = new Vector2 (mousePos.x - oldOldMousePos.x, mousePos.y - oldOldMousePos.y);
			float isClockwise = (oldOldToOld.x * oldOldToCurr.y) - (oldOldToOld.y * oldOldToCurr.x);
			
			float xDiff = mousePos.x - oldMousePos.x;
			float yDiff = mousePos.y - oldMousePos.y;
			float mag = Mathf.Sqrt ((xDiff * xDiff) + (yDiff * yDiff));
			
			if (isClockwise < 0) {
				mag = -mag;
			}
			
			//wheel1.transform.Rotate(Vector3.back, mag * 10);
			
			float aVelDiff = aVel - mag;
			aVel -= aVelDiff;
			wheel1.transform.Rotate(Vector3.back, Mathf.Abs(aVel) * 30);
			wheel2.transform.Rotate(Vector3.back, Mathf.Abs(aVel) * 30);
			
			//transform.rigidbody.angularVelocity -= new Vector3 (0, 0, .001f * aVelDiff);
			aTotal += aVel;
			
			if (aTotal > 200){
				stateMachine.SwitchStates(finishedState);
			}
		}
	}
	
	void bicycleEnter() {}
	void bicycleExit() {}
	
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
}
