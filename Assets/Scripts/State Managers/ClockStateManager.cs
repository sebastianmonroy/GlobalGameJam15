using UnityEngine;
using System.Collections;

public class ClockStateManager : FrameStateManager {

	SimpleState ticktockState, finishedState;	
	
	

	void Start() 
	{
		ticktockState = new SimpleState(TickTockEnter, TickTockUpdate, TickTockExit, "TICK TOCK");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		
		stateMachine.SwitchStates(ticktockState);
	}
	
	#region TICKTOCK
	public GameObject MinuteHand;
	public GameObject HourHand;
	private Timer secondTimer;
	private float second;
	private float minute;
	private float hour;

	void TickTockEnter() 
	{
		secondTimer = new Timer(1.0f);
		secondTimer.Repeat();
		secondTimer.Restart();
	}

	void TickTockUpdate() 
	{
		if (secondTimer.IsFinished())
		{
			IncrementTime();

			StartCoroutine(tick());
		}
	}
	void TickTockExit() {}
	#endregion

		void IncrementTime()
		{
			second++;
			if (second >= 60)
			{
				second -= 60;
				minute++;
			}

			if (minute >= 60)
			{
				minute -= 60;
				hour++;
			}

			if (hour >= 12)
			{
				hour -= 12;
			}
		}

		IEnumerator tick()
		{
			Quaternion minuteStartRotation = MinuteHand.transform.localRotation;
			Quaternion minuteEndRotation = minuteStartRotation * Quaternion.AngleAxis(360f/60f, Vector3.up);
			Quaternion hourStartRotation = HourHand.transform.localRotation;
			Quaternion hourEndRotation = hourStartRotation * Quaternion.AngleAxis(360f/60f/60f, Vector3.up);

			Timer timer = new Timer(0.1f);
			while (!timer.IsFinished())
			{
				MinuteHand.transform.localRotation = Quaternion.Lerp(minuteStartRotation, minuteEndRotation, timer.Percent());
				HourHand.transform.localRotation = Quaternion.Lerp(hourStartRotation, hourEndRotation, timer.Percent());
				
				yield return 0;
			}
		}
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
