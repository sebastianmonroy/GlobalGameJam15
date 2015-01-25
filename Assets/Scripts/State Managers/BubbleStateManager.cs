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
		
		foreach(Finger f in GestureHandler.instance.fingers){
			if(f.hitObject == GO1){
				GO1.transform.position = f.GetWorldPosition3();
			} else if (f.hitObject == GO2){
				GO2.transform.position = f.GetWorldPosition3();
			}
		}

		if (Vector3.Distance(GO1.transform.position, GO2.transform.position) <= GO1.transform.localScale.x)
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	void floatExit() {}
	#endregion
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
