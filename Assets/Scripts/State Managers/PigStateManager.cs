using UnityEngine;
using System.Collections;

public class PigStateManager : FrameStateManager {

	SimpleState floatState, finishedState;
	public GameObject ear1, ear2;
	public Transform shrink1, shrink2, oPos1, oPos2;
	
	void Start() 
	{
		floatState = new SimpleState(floatEnter, floatUpdate, floatExit, "FLOAT");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(floatState);
	}
	
	#region FLOAT
	void floatEnter() {}
	void floatUpdate() {
		
		foreach(Finger f in GestureHandler.instance.fingers)
		{
			if (f.hitObject != null)
			{
				if (f.hitObject.transform.parent.gameObject == ear1 || f.hitObject.transform.parent.gameObject == ear2)
				{
					ear1.transform.position = Vector2.Lerp(oPos1, shrink1, Time.time);
					ear2.transform.position = Vector2.Lerp(oPos2, shrink2, Time.time);
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
