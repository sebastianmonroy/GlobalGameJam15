using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SexStateManager : FrameStateManager {
	
	SimpleState sexState, finishedState1, finishedState2;
	public GameObject MaleRoot, FemaleRoot;

	float maleP = 1;
	float femaleP = 0;

	Vector3 dragOffset;
	
	void Start() 
	{
		//Debug.Log (GetClosestPointOnLine(Vector3.zero, Vector3.up * 5, new Vector3(.5f,.5f,0)));
		sexState = new SimpleState(sexEnter, sexUpdate, sexExit, "SEX");
		finishedState1 = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED1");
		finishedState2 = new SimpleState(finishedEnter, finishedUpdate, finishedExit, "FINISHED2");

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
				if (f.hitObject.tag == "GenderSymbol"){

					Vector3 lineStart = f.hitObject.transform.parent.FindChild("lineStart").transform.position;
					Vector3 lineEnd = f.hitObject.transform.parent.FindChild("lineEnd").transform.position;

					Vector3 pos = GetClosestPointOnLine(lineStart, lineEnd, new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, f.hitObject.transform.position.z));
					pos.x = Mathf.Clamp(pos.x, Mathf.Min(lineStart.x, lineEnd.x), Mathf.Max(lineStart.x, lineEnd.x));
					pos.y = Mathf.Clamp(pos.y, Mathf.Min(lineStart.y, lineEnd.y), Mathf.Max(lineStart.y, lineEnd.y));

					f.hitObject.transform.position = pos;

					float percent = ((pos - lineStart).magnitude)/((lineEnd-lineStart).magnitude);

					Transform left = f.hitObject.transform.FindChild("arrow 2");
					Transform right = f.hitObject.transform.FindChild("arrow 3");

					Vector3 rot = Vector3.zero;
					rot.z = -45f*(percent+.01f);
					//Debug.Log(percent+.001f);


					left.localEulerAngles = rot;
					right.localEulerAngles = -rot;

					Color c = Color.Lerp(new Color(255f/255f,173f/255f,154f/255f,1), new Color(150f/255f,177f/255f,255f/255f,1), percent);

					MeshRenderer[] rends = f.hitObject.transform.parent.GetComponentsInChildren<MeshRenderer>();
					foreach (MeshRenderer rend in rends){
						rend.material.color = c;

						//Debug.Log(c);
					}

//					Debug.Log(f.hitObject.transform.parent);
					if (f.hitObject.transform.parent.name == "Male"){
						maleP = percent;
					}
					else if (f.hitObject.transform.parent.name == "Female"){
						femaleP = percent;
					}

				}

				else if (f.hitObject == Polygons[0] || f.hitObject == Polygons[1]){
					f.hitObject.transform.parent.position = new Vector3(f.GetWorldPosition().x, f.GetWorldPosition().y, f.hitObject.transform.parent.position.z);

				}
			






			}
		}
		
		if (Vector3.Distance(MaleRoot.transform.position, FemaleRoot.transform.position) <= 1f)
		{
			Debug.Log(maleP + femaleP);

			if (maleP + femaleP > .5f && maleP + femaleP < 1.5f){
				stateMachine.SwitchStates(finishedState1);
			}
		else {
				stateMachine.SwitchStates(finishedState2);

			}

		}
	}

	static public Vector3 GetClosestPointOnLine(Vector3 start, Vector3 end, Vector3 point){
		Vector3 ab = end - start;
		ab = ab.normalized;
		Vector3 ap = point - start;

		Vector3 x = start + (Vector3.Dot (ap, ab)) * ab.normalized;

		return x;
	}


	void sexExit() {}
	#endregion
	
	#region FINISHED
	void finishedEnter() {}
	void finishedUpdate() {}
	void finishedExit() {}
	#endregion
}
