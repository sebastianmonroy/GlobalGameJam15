using UnityEngine;
using System.Collections;

public class BubbleStateManager : FrameStateManager {

	SimpleState floatState, finishedState;
	public GameObject BigBubble, SmallBubble;
	
	void Start() 
	{
		floatState = new SimpleState(floatEnter, floatUpdate, floatExit, "FLOAT");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(floatState);
	}
	
	#region FLOAT
	void floatEnter() {}
	void floatUpdate() {
		BigBubble.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/8, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/5, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/7, 0.1f)+0.9f
		);
		SmallBubble.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/14, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/8, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/10, 0.1f)+0.9f
		);
		
		foreach(Finger f in GestureHandler.instance.fingers)
		{
			if (f.hitObject != null)
			{
				if (f.hitObject.transform.parent.gameObject == BigBubble)
				{
					BigBubble.transform.position = new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, BigBubble.transform.position.z);
				} 
				else if (f.hitObject.transform.parent.gameObject == SmallBubble)
				{
					SmallBubble.transform.position = new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, SmallBubble.transform.position.z);
				}
			}
		}

		if (Vector3.Distance(BigBubble.transform.position, SmallBubble.transform.position) <= 1f)
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
