using UnityEngine;
using System.Collections;

public class StartScreenManager : FrameStateManager {

	SimpleState menuState, finishedState;
	
	void Start() 
	{
		menuState = new SimpleState(menuEnter, menuUpdate, menuExit, "MENU");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED_STARTSCREEN");
		
		stateMachine.SwitchStates(menuState);
	}
	
	#region FLOAT
	void menuEnter() {}
	void menuUpdate() {
		if( GestureHandler.instance.fingers.Count > 0 )
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	void menuExit() {}
	#endregion
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
