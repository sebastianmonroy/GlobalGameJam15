using UnityEngine;
using System.Collections;

public class ClockStateManager : FrameStateManager {

	SimpleState ticktockState, finishedState, finishedState2;	
	
	

	void Start() 
	{
		ticktockState = new SimpleState(TickTockEnter, TickTockUpdate, TickTockExit, "TICK TOCK");
		finishedState = new SimpleState(null, null, null, "FINISHED_CLOCK1");
		finishedState2 = new SimpleState(null, null, null, "FINISHED_CLOCK2");
		
		stateMachine.SwitchStates(ticktockState);
	}
	
	#region TICKTOCK
	public GameObject MinuteHand;
	public GameObject HourHand;
	public GameObject Face;
	private Timer secondTimer;
	private float second;
	private float minute;
	private float hour;
	Vector3 lastMinPosition = Vector3.zero;
	public GameObject fingerCollider, minuteQuad;

	void TickTockEnter() 
	{
		secondTimer = new Timer(1.0f);
		secondTimer.Repeat();
		secondTimer.Restart();
	}

	void TickTockUpdate() 
	{
		if(GestureHandler.instance.fingers.Count > 0){	
			Finger f = GestureHandler.instance.fingers[0] as Finger;
			fingerCollider.transform.localPosition = f.GetWorldPosition();
			Vector3 dir = minuteQuad.transform.right;
			dir = new Vector3(dir.y, -dir.x, dir.z);
			RaycastHit2D hit = Physics2D.Raycast(MinuteHand.transform.position, dir);
			//Debug.DrawRay(Decorations[1].transform.position, dir, Color.red);
			
			/*if(hit.collider != null) {
				if (hit.collider.gameObject.name == "fingerPos"){
					Debug.Log("looking at finger");
					//facing finger position
					secondTimer.Stop();
					secondTimer = new Timer(1.0f);
					secondTimer.Repeat();
				}
			} else {	*/
				Debug.Log("moving clockwise");
				float speed = 150f;
				secondTimer.Stop();
				MinuteHand.transform.RotateAround(Face.transform.position, -Vector3.forward, speed* Time.deltaTime);
				HourHand.transform.RotateAround(Face.transform.position, -Vector3.forward, (speed/12f)* Time.deltaTime);
				secondTimer = new Timer(1.0f);
				secondTimer.Repeat();
				if(f.GetLifeSpan() > 2f) stateMachine.SwitchStates(finishedState2);
			//}
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
}
