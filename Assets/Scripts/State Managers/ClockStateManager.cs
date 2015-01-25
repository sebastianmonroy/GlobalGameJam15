using UnityEngine;
using System.Collections;

public class ClockStateManager : FrameStateManager {

	SimpleState ticktockState, finishedState, finishedStateIceCream;	
	
	

	void Start() 
	{
		ticktockState = new SimpleState(TickTockEnter, TickTockUpdate, TickTockExit, "TICK TOCK");
		finishedState = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED");
		finishedStateIceCream = new SimpleState(finishedIceCreamEnter, finishedIceCreamUpdate, finishedIceCreamExit, "FINISHEDICECREAM");
		
		stateMachine.SwitchStates(ticktockState);
	}
	
	#region TICKTOCK
	private GameObject MinuteHand;
	private GameObject HourHand;
	private Timer secondTimer;
	private float second;
	private float minute;
	private float hour;

	void TickTockEnter() 
	{
		MinuteHand = DynamicDecorations[0];
		HourHand = DynamicDecorations[1];

		secondTimer = new Timer(1.0f);
		secondTimer.Repeat();
		secondTimer.Restart();
	}

	void TickTockUpdate() 
	{
		foreach(Finger f in GestureHandler.instance.fingers){
			if(f.hitObject) {
				if(f.hitObject.transform.parent.name == "Minute Hand" || f.hitObject.transform.parent.name == "Hour Hand"){
					IncrementTime();
					Timer timer = new Timer(0.25f);
					StartCoroutine(tick(timer));
				}
			}
		}
		
		if (secondTimer.IsFinished())
		{
			IncrementTime();
			Timer timer = new Timer(0.1f);
			StartCoroutine(tick(timer));
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

	IEnumerator tick(Timer timer)
	{
		Quaternion minuteStartRotation = MinuteHand.transform.localRotation;
		Quaternion minuteEndRotation = minuteStartRotation * Quaternion.AngleAxis(360f/60f, Vector3.up);
		Quaternion hourStartRotation = HourHand.transform.localRotation;
		Quaternion hourEndRotation = hourStartRotation * Quaternion.AngleAxis(360f/60f/60f, Vector3.up);

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
	
	#region FINISHEDICECREAM
	void finishedIceCreamEnter() {}
	void finishedIceCreamUpdate() {}
	void finishedIceCreamExit() {}
	#endregion
}
