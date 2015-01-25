using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaterpillarStateManager : FrameStateManager
{
	SimpleState idleState, finishedState;	
	
	void Start() 
	{
		idleState = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "IDLE");
		finishedState = new SimpleState(null, null, null, "FINISHED_CATERPILLAR");

		stateMachine.SwitchStates(idleState);
	}
	
	#region IDLE
	public GameObject head, segment1, segment2, tail;

	void IdleEnter() 
	{

	}
	
	void IdleUpdate() 
	{
		bool headGrabbed = false, tailGrabbed = false;
		Finger headFinger = new Finger(), tailFinger = new Finger();
		foreach (Finger finger in GestureHandler.instance.fingers)
		{
			if (finger.hitObject != null && finger.hitObject.transform.parent == head.transform && !headGrabbed)
			{
				headFinger = finger;
				headGrabbed = true;
			}
			else if (finger.hitObject == tail && !tailGrabbed)
			{
				tailFinger = finger;
				tailGrabbed = true;
			}
		}

		if (tailGrabbed && headGrabbed)
		{
			tail.transform.position = new Vector3(tailFinger.GetWorldPosition().x, tailFinger.GetWorldPosition().y, tail.transform.position.z);
			head.transform.position = new Vector3(headFinger.GetWorldPosition().x, headFinger.GetWorldPosition().y, head.transform.position.z);
			Vector3 tailToHead = head.transform.position - tail.transform.position;
			
			Vector3 newSegment1Position = tail.transform.position + tailToHead / 3f;
			newSegment1Position.z = segment1.transform.position.z;
			segment1.transform.position = newSegment1Position;

			Vector3 newSegment2Position = tail.transform.position + 2f * tailToHead / 3f;
			newSegment2Position.z = segment2.transform.position.z;
			segment2.transform.position = newSegment2Position;

			if (Vector3.Distance(tail.transform.position, head.transform.position) > 5f)
			{
				stateMachine.SwitchStates(finishedState);
			}
		}
		else if (tailGrabbed)
		{
			if (Vector3.Distance(tailFinger.GetWorldPosition(), head.transform.position) < 3.5f)
			{
				tail.transform.position = new Vector3(tailFinger.GetWorldPosition().x, tailFinger.GetWorldPosition().y, tail.transform.position.z);
				Vector3 tailToHead = head.transform.position - tail.transform.position;
				
				Vector3 newSegment1Position = tail.transform.position + tailToHead / 3f;
				newSegment1Position.z = segment1.transform.position.z;
				segment1.transform.position = newSegment1Position;

				Vector3 newSegment2Position = tail.transform.position + 2f * tailToHead / 3f;
				newSegment2Position.z = segment2.transform.position.z;
				segment2.transform.position = newSegment2Position;
			}
		}
		else if (headGrabbed)
		{
			if (Vector3.Distance(tail.transform.position, headFinger.GetWorldPosition()) < 3.5f)
			{
				head.transform.position = new Vector3(headFinger.GetWorldPosition().x, headFinger.GetWorldPosition().y, head.transform.position.z);
				Vector3 tailToHead = head.transform.position - tail.transform.position;
				
				Vector3 newSegment1Position = tail.transform.position + tailToHead / 3f;
				newSegment1Position.z = segment1.transform.position.z;
				segment1.transform.position = newSegment1Position;

				Vector3 newSegment2Position = tail.transform.position + 2f * tailToHead / 3f;
				newSegment2Position.z = segment2.transform.position.z;
				segment2.transform.position = newSegment2Position;
			}
		}
		else
		{

		}
	}
	
	void IdleExit() {}
	#endregion
	
}
