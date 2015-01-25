using UnityEngine;
using System.Collections;

public class SpaceStateManager : FrameStateManager {

	SimpleState floatState, finishedState;
	public GameObject planet1, planet2, planet3, sun;
	private bool select1, select2, select3;
	
	void Start() 
	{
		floatState = new SimpleState(floatEnter, floatUpdate, floatExit, "FLOAT");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(floatState);
	}
	
	#region FLOAT
	void floatEnter() {}
	void floatUpdate() {
		/*BigBubble.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/8, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/5, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/7, 0.1f)+0.9f
		);
		SmallBubble.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/14, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/8, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/10, 0.1f)+0.9f
		);*/
		
		foreach(Finger f in GestureHandler.instance.fingers)
		{
			if (f.hitObject != null)
			{
				if (f.hitObject.transform.parent.gameObject == planet1)
				{
					select1 = true;
				} else {
					select1 = false;
					planet1.transform.RotateAround(sun.transform.position, Time.time);
				}
				
				if (f.hitObject.transform.parent.gameObject == planet1)
				{
					select2 = true;
				} else {
					select2 = false;
					planet2.transform.RotateAround(sun.transform.position, Time.time*2);
				}
				
				if (f.hitObject.transform.parent.gameObject == planet1)
				{
					select3 = true;
				} else {
					select3 = false;
					planet3.transform.RotateAround(sun.transform.position, Time.time*4);
				}
			}
		}

		if (select1 && select2 && select3)
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
