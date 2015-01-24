using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class GestureHandler : MonoBehaviour 
{
	public static GestureHandler instance;
	public int fingerCount = 0;
	public List<Finger> fingers = new List<Finger>();
	public bool debugFingers;
	public float dtError = 0.5f;
	public float doubleTapTime = 0.5f;

	void Start()
	{
		instance = this;
	}

	void Update() 
	{
		// Populate list of all current touches
		List<Touch> newTouches = new List<Touch>();
		foreach (Touch touch in Input.touches) {
			newTouches.Add(touch);
		}

		// Update existing Fingers using reported touches
		foreach (Finger finger in fingers) 
		{
			bool found = false;
			foreach (Touch touch in Input.touches) 
			{
				if (touch.fingerId == finger.id) 
				{
					Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
					if(Mathf.Abs(finger.position.x - touchPos.x) < dtError && Mathf.Abs(finger.position.y - touchPos.y) < dtError && finger.upTime > 0f && finger.upTime < doubleTapTime){
						Debug.Log("DOUBLE TAP");
						finger.upTime = doubleTapTime;
					} else {
						finger.Update(touch);
						newTouches.Remove(touch);
						found = true;
					}
				}
			}

			if (!found) 
			{
				if(finger.upTime > doubleTapTime) {
					finger.isValid = false;
				} else {
					finger.upTime += 0.1f;
				}
			}
		}

		CleanFingers();

		// Make new Fingers for remaining unaccounted-for touches
		foreach (Touch touch in newTouches) 
		{
			Finger newFinger = new Finger(touch);
			fingers.Add(newFinger);
		}

		fingerCount = fingers.Count;

		PrintAllFingers();
	}

	void PrintAllFingers() 
	{
		if (debugFingers)
		{
			if (fingerCount > 0) 
			{
				string output = "";
				output += "+-----------" + fingerCount + " Fingers-----------+\n";
				foreach (Finger finger in fingers) 
				{
					output += "|\t" + finger.ToString() + "\n";
				}
				output += "+--------------------------------+";
				print(output);
			} 
			else 
			{
				//Debug.Log("########### 0 Fingers ###########");
			}
		}
	}

	void CleanFingers() 
	{
		// Clean up invalid Fingers
		List<Finger> valids = new List<Finger>();
		foreach (Finger finger in fingers)
		{
			if (finger.isValid)
			{
				valids.Add(finger);
			}
		}

		fingers = valids;
	}
}
