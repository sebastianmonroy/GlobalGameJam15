using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SexStateManager : FrameStateManager {
	
	SimpleState sexState, finishedState;
	public GameObject MaleRoot, FemaleRoot;
	
	void Start() 
	{
		sexState = new SimpleState(sexEnter, sexUpdate, sexExit, "SEX");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(sexState);
	}
	
	#region FLOAT
	void sexEnter() {}
	void sexUpdate() {
		MaleRoot.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/8, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/5, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/7, 0.1f)+0.9f
			);
		FemaleRoot.transform.localScale = new Vector3(
			Mathf.PingPong(Time.time/14, 0.07f)+0.93f,
			Mathf.PingPong(Time.time/8, 0.15f)+0.85f,
			Mathf.PingPong(Time.time/10, 0.1f)+0.9f
			);
		
		foreach(Finger f in GestureHandler.instance.fingers)
		{
			if (f.hitObject != null)
			{
				if (f.hitObject.transform.parent.gameObject == MaleRoot)
				{
					MaleRoot.transform.position = new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, MaleRoot.transform.position.z);
				} 
				else if (f.hitObject.transform.parent.gameObject == FemaleRoot)
				{
					FemaleRoot.transform.position = new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, FemaleRoot.transform.position.z);
				}
			}
		}
		
		if (Vector3.Distance(MaleRoot.transform.position, FemaleRoot.transform.position) <= 1f)
		{
			//stateMachine.SwitchStates(finishedState);
		}
	}
	void sexExit() {}
	#endregion
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
