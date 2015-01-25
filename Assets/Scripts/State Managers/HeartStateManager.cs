using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeartStateManager : FrameStateManager
{
	SimpleState ticktockState, finishedState;	
	float scaleX, scaleY;
	public GameObject heart;
	Timer secondTimer;
	public AnimationCurve bump;
	
	void Start() 
	{
		ticktockState = new SimpleState(TickTockEnter, TickTockUpdate, TickTockExit, "TICK TOCK");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED_HEART");
		
		stateMachine.SwitchStates(ticktockState);
		scaleX = heart.transform.localScale.x;
		scaleY = heart.transform.localScale.y;
	}
	
	#region TICKTOCK
	Timer overallTimer = new Timer(7.0f);
	
	void TickTockEnter() 
	{
		secondTimer = new Timer(1.0f);
		secondTimer.Repeat();
		secondTimer.Restart();
	}
	
	void TickTockUpdate() 
	{
		if (secondTimer.Percent() >= 1f)
		{
			Debug.Log("update");
			IncrementTime();
			secondTimer.Restart();
			StartCoroutine(tick());
		}

		if (overallTimer.Percent() >= 1f)
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	void TickTockExit() {}
	#endregion
	
	void IncrementTime()
	{
		
	}
	
	IEnumerator tick()
	{
		while (!secondTimer.IsFinished())
		{
			heart.transform.localScale = new Vector3(scaleX + bump.Evaluate(secondTimer.Percent()), scaleY + bump.Evaluate(secondTimer.Percent()), heart.transform.localScale.z);

			yield return 0;
		}
	}
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
