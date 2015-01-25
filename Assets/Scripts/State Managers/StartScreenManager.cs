using UnityEngine;
using System.Collections;

public class StartScreenManager : FrameStateManager {

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
		if( GestureHandler.instance.fingers.Count > 0 )
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
