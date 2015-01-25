using UnityEngine;
using System.Collections;

public class IceCreamStateManager : FrameStateManager {

	SimpleState meltState, fallState, shimmerState, finishedState;	
	
	void Start() 
	{
		meltState = new SimpleState(MeltEnter, MeltUpdate, MeltExit, "MELT");
		finishedState = new SimpleState(null, null, null, "FINISHED");

		stateMachine.SwitchStates(meltState);
	}
	
	public void Execute() {}
	
	#region MELT
	void MeltEnter() {}
	void MeltUpdate() {}
	void MeltExit() {}
	#endregion
	
}
