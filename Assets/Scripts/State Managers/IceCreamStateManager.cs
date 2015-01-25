using UnityEngine;
using System.Collections;

public class IceCreamStateManager : FrameStateManager {

	SimpleState meltState, finishedState;	
	
	void Start() 
	{
		meltState = new SimpleState(MeltEnter, MeltUpdate, MeltExit, "MELT");
		finishedState = new SimpleState(null, null, null, "FINISHED_ICECREAM");

		stateMachine.SwitchStates(meltState);
	}
	
	#region MELT
	public GameObject vanilla;

	void MeltEnter() {}
	
	void MeltUpdate() 
	{
		foreach (Finger finger in GestureHandler.instance.fingers)
		{
			if (finger.hitObject == vanilla && Vector2.Dot(finger.velocity, Vector2.up) > 10f)
			{
				vanilla.transform.localScale *= 0.975f;
				break;
			}
		}

		if (vanilla.transform.localScale.x <= 0.75f)
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	
	void MeltExit() 
	{
		//Destroy(vanilla);
	}
	#endregion
	
}
