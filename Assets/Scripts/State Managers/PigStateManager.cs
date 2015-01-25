using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PigStateManager : FrameStateManager {

	SimpleState idleState, finishedState;	
	
	void Start() 
	{
		idleState = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "IDLE");
		finishedState = new SimpleState(null, null, null, "FINISHED_PIG");

		stateMachine.SwitchStates(idleState);
	}
	
	#region IDLE
	void IdleEnter() 
	{

	}
	
	void IdleUpdate() 
	{
		
	}
	
	void IdleExit() {}
	#endregion
	
}
