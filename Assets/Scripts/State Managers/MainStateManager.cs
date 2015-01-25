﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;
	SimpleState clockState, bubbleState, icecreamState, sexState, bicycleState, pawState, startScreenState;
	SimpleState finishedState;

	public GameObject machineObject;
	public FrameStateManager machine;
	public GameObject icecream, bubble, clock, sex, bicycle, paw, startScreen;

	public bool initialized = false;

	void Start()
	{
		Init();
	}

	public void Init () 
	{
		bubbleState = new SimpleState(BubbleEnter, BubbleUpdate, BubbleExit, "BUBBLE");
		clockState = new SimpleState(ClockEnter, ClockUpdate, ClockExit, "CLOCK");
		icecreamState = new SimpleState(IceCreamEnter, IceCreamUpdate, IceCreamExit, "ICE CREAM");
		sexState = new SimpleState(SexEnter, SexUpdate, SexExit, "SEX");
		bicycleState = new SimpleState(BicycleEnter, BicycleUpdate, BicycleExit, "BICYCLE");
		pawState = new SimpleState(PawEnter, PawUpdate, PawExit, "PAW");
		startScreenState = new SimpleState(startScreenEnter, startScreenUpdate, startScreenExit, "STARTSCREEN");

		Setup();
	}
	
	void Update () 
	{
		if (!initialized)
		{
			Setup();
		}
		else 
		{
			Execute();
		}
	}

	public void Setup () 
	{
		if (BlendHandler.Instance != null)
		{
			machine = machineObject.GetComponent<FrameStateManager>();
			machine.EnableShapes();
			machine.EnableLight();
			machine.gameObject.SetActive(true);

			BlendHandler.Instance.Background.renderer.material.color = machine.BackgroundColor;

			Switch(startScreenState, startScreen, 1.0f);

			this.initialized = true;
		}
	}

	public bool switching = false;
	public GameObject nextMachineObject;
	public FrameStateManager nextMachine;

	public void Switch(SimpleState nextState, GameObject nextObject, float duration)
	{
		stateMachine.SwitchStates(nextState);

		nextMachineObject = Instantiate(nextObject, Vector3.zero, Quaternion.identity) as GameObject;
		nextMachine = nextMachineObject.GetComponent<FrameStateManager>();
		nextMachine.DisableShapes();
		nextMachine.DisableLight();

		BlendHandler.Instance.Blend(machine, nextMachine, duration);

		StartCoroutine(AfterTransition(duration));
	}

		IEnumerator AfterTransition(float duration)
		{
			switching = true;
			Timer timer = new Timer(duration);
			while (timer.Percent() < 1f)
			{
				yield return 0;
			}
			switching = false;

			FrameStateManager oldMachine = machine;

			machine = nextMachine;
			machineObject = nextMachineObject;
			machine.EnableShapes();

			oldMachine.DisableShapes();
			oldMachine.DisableLight();
			machine.EnableLight();
			Destroy(oldMachine.gameObject);
		}

	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region BUBBLE
	void BubbleEnter() {
		
	}
	
	void BubbleUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
			Switch(clockState, clock, 1.0f);
		}
	}
	
	void BubbleExit() {}
	#endregion
	
	#region BUBBLE
	void startScreenEnter() {
		
	}
	
	void startScreenUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
			Switch(bubbleState, bubble, 1.0f);
		}
	}
	
	void startScreenExit() {}
	#endregion

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		machine.Execute();

		if (machine.stateMachine.currentState == "FINISHED")
		{
			//Switch(personState, icecream, 3.0f);
		} else if (machine.stateMachine.currentState == "FINISHEDICECREAM")
		{
			Switch(icecreamState, icecream, 1.0f);
		}
	}

	void ClockExit() {}
	#endregion
	
	#region ICECREAM
	void IceCreamEnter() {}
	
	void IceCreamUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED" || Input.GetKeyDown(KeyCode.Space))
		{
			Switch(sexState, sex, 1.0f);
		}
	}
	
	void IceCreamExit() {}
	#endregion

	#region SEX
	void SexEnter() {}
	
	void SexUpdate() 
	{
		machine.Execute();
		Debug.Log("here");

		if (machine.stateMachine.currentState == "FINISHED1" || Input.GetKeyDown(KeyCode.Space))
		{
			Switch(bicycleState, bicycle, 1.0f);
		}
		if (machine.stateMachine.currentState == "FINISHED2")
		{
			Switch(icecreamState, icecream, 1.0f);
		}
	}
	
	void SexExit() {}
	#endregion

	#region BICYCLE
	void BicycleEnter() {}
	
	void BicycleUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
		
		}
	}
	
	void BicycleExit() {}
	#endregion

	#region PAW
	void PawEnter() {}
	
	void PawUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
			
		}
	}
	
	void PawExit() {}
	#endregion
}
