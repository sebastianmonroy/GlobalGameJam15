using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceStateManager : FrameStateManager {

	SimpleState idleState, finishedState;	
	
	public GameObject planet1, planet2, planet3;

	void Start() 
	{
		idleState = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "IDLE");
		finishedState = new SimpleState(null, null, null, "FINISHED_SPACE");

		stateMachine.SwitchStates(idleState);
	}
	
	#region IDLE
	private float speed1 = 15f, speed2 = 25f, speed3 = 35f;
	private bool touched1 = false, touched2 = false, touched3 = false;
	void IdleEnter() {}
	
	void IdleUpdate() 
	{
		planet1.transform.RotateAround(Vector3.zero, Vector3.back, speed1 * Time.deltaTime);
		planet2.transform.RotateAround(Vector3.zero, Vector3.back, speed2 * Time.deltaTime);
		planet3.transform.RotateAround(Vector3.zero, Vector3.back, speed3 * Time.deltaTime);

		foreach (Finger finger in GestureHandler.instance.fingers)
		{
			if (finger.hitObject == planet1 && !touched1)
			{
				speed1 *= 5f;
				touched1 = true;
			}
			else if (finger.hitObject == planet2 && !touched2)
			{
				speed2 *= 5f;
				touched2 = true;
			}
			else if (finger.hitObject == planet3 && !touched3)
			{
				speed3 *= 5f;
				touched3 = true;
			}
		}

		if (touched1 && touched2 && touched3)
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	
	void IdleExit() {}
	#endregion
	
}
