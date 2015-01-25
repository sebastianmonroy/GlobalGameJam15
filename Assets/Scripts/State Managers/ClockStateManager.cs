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
	Vector3 lastMinPosition = Vector3.zero;

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
		if(GestureHandler.instance.fingers.Count > 0){	
			Finger f = GestureHandler.instance.fingers[0] as Finger;
			RaycastHit2D hit = Physics2D.Raycase(DynamicDecorations[0].transform.position, Vector2.forward);
			if(hit.collider != null) {
				if(hit.collider.gameObject.name = "noon") {
					//facing noon position
					Timer hourTimer = new Timer(5.0f);
					StartCoroutine(HourPassed(hourTimer));
				} else if (hit.collider.gameObject.name = "fingerPos"){
					//facing finger position
					secondTimer.Stop();
					secondTimer = new Timer(1.0f);
					secondTimer.Repeat();
				}
			} else {	
				secondTimer.Stop();
				DynamicDecorations[0].transform.RotateAround(PolygonObjects[1].transform.position, -Vector3.forward, 150* Time.deltaTime);
				secondTimer = new Timer(1.0f);
				secondTimer.Repeat();
			}
		}
		
		if (secondTimer.IsFinished())
		{
			Timer timer = new Timer(0.1f);
			float endAmt = 360f/60f;
			IncrementTime();
			StartCoroutine(tick(timer, endAmt));
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

	IEnumerator tick(Timer timer, float endAmt = 360f/60f)
	{
		Quaternion minuteStartRotation = MinuteHand.transform.localRotation;
		Quaternion minuteEndRotation = minuteStartRotation * Quaternion.AngleAxis(endAmt, Vector3.up);
		Quaternion hourStartRotation = HourHand.transform.localRotation;
		Quaternion hourEndRotation = hourStartRotation * Quaternion.AngleAxis(endAmt/60f, Vector3.up);

		while (!timer.IsFinished())
		{
			MinuteHand.transform.localRotation = Quaternion.Lerp(minuteStartRotation, minuteEndRotation, timer.Percent());
			HourHand.transform.localRotation = Quaternion.Lerp(hourStartRotation, hourEndRotation, timer.Percent());
			
			yield return 0;
		}
		
	}
	
	IEnumerator HourPassed(Timer timer){
		Debug.Log("hour passed");
		Quaternion hourStartRotation = HourHand.transform.localRotation;
		Quaternion hourEndRotation = hourStartRotation * Quaternion.AngleAxis(360f/60f/60f, Vector3.up);
		
		while (!timer.IsFinished())
		{
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
