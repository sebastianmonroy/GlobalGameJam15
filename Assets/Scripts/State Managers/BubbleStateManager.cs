using UnityEngine;
using System.Collections;

public class BubbleStateManager : FrameStateManager {

	SimpleState floatState, finishedState;	
	
	void Start() 
	{
		floatState = new SimpleState(floatEnter, floatUpdate, floatExit, "FLOAT");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(floatState);
	}
	
	#region FLOAT
	void floatEnter() {}
	void floatUpdate() {
		GameObject GO1 = PolygonObjects[1] as GameObject;
		GameObject GO2 = PolygonObjects[0] as GameObject;
		GO1.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/8, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/5, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/7, 0.1f)+0.9f
		);
		GO2.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/14, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/8, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/10, 0.1f)+0.9f
		);
		
		
	}
	void floatExit() {}
	#endregion
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
