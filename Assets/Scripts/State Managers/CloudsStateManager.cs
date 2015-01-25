using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudsStateManager : FrameStateManager {

	SimpleState puffState, finishedState;	
	
	void Start() 
	{
		puffState = new SimpleState(PuffEnter, PuffUpdate, PuffExit, "MELT");
		finishedState = new SimpleState(null, null, null, "FINISHED_CLOUDS");

		stateMachine.SwitchStates(puffState);
	}
	
	#region PUFF
	public GameObject grayCloud;
	public List<GameObject> backgroundClouds = new List<GameObject>();
	private Finger tapping = new Finger();
	private float scaling = 1f;

	void PuffEnter() {}
	
	void PuffUpdate() 
	{
		foreach (GameObject go in backgroundClouds)
		{
			go.transform.position -= Vector3.right * 0.5f * Time.deltaTime;
		}
		foreach (Finger finger in GestureHandler.instance.fingers)
		{
			if (finger.hitObject != null && finger.hitObject.transform.parent.gameObject == grayCloud)
			{
				if (tapping == null || tapping != finger)
				{
					grayCloud.transform.localScale *= 1.1f;
					scaling *= 1.1f;
					tapping = finger;
					break;
				}
			}
		}

		if (scaling > 2f)
		{
			stateMachine.SwitchStates(finishedState);
		}
	}
	
	void PuffExit() 
	{
		StartCoroutine(Shrink());
	}
	#endregion
	
		IEnumerator Shrink()
		{
			Timer timer = new Timer(0.5f);
			Vector3 startScale = grayCloud.transform.localScale;
			Vector3 endScale = grayCloud.transform.localScale / 2f;
			while (timer.Percent() < 1f)
			{
				grayCloud.transform.localScale = Vector3.Lerp(startScale, endScale, timer.Percent());
				yield return 0;
			}
		}
}
