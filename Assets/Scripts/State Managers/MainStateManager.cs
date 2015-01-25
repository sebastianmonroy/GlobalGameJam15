using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;
	SimpleState clockState, bubbleState, icecreamState;
	SimpleState finishedState;

	public GameObject machineObject;
	public FrameStateManager machine;
	public IceCreamStateManager icecreamMachine;
	public BubbleStateManager bubbleMachine;
	public ClockStateManager clockMachine;

	void Start()
	{
		Init();
	}

	public void Init () 
	{
		bubbleState = new SimpleState(BubbleEnter, BubbleUpdate, BubbleExit, "BUBBLE");
		clockState = new SimpleState(ClockEnter, ClockUpdate, ClockExit, "CLOCK");
		icecreamState = new SimpleState(IceCreamEnter, IceCreamUpdate, IceCreamExit, "ICE CREAM");

		/*
		playState = new SimpleState(PlayEnter, PlayUpdate, PlayExit, "PLAY");
		disconnectState = new SimpleState(DisconnectEnter, DisconnectUpdate, DisconnectExit, "DISCONNECT");
		finishedState = new SimpleState(null, null, null, "FINISHED");
		*/

		Setup();
	}
	
	void Update () 
	{
		Execute();
	}

	public void Setup () 
	{
		SetMachine(bubbleMachine);
		BlendHandler.Instance.Background.renderer.material.color = bubbleMachine.BackgroundColor;
		stateMachine.SwitchStates(bubbleState);

	}

	public void SetMachine(FrameStateManager toMachine)
	{
		machineObject = Instantiate(toMachine.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		machine = machineObject.GetComponent<FrameStateManager>();
		machine.EnableAll();
	}

	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		machine.Execute();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			stateMachine.SwitchStates(icecreamState);
		}
	}

	void ClockExit() 
	{
		BlendHandler.Instance.Blend(machine, icecreamMachine, 3.0f);
		SetMachine(icecreamMachine);
		//machineObject = Instantiate(icecreamMachine.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
	}
	#endregion
	
	#region ICECREAM
	void IceCreamEnter() {
	
	}
	
	void IceCreamUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
		
		}
	}
	
	void IceCreamExit() {}
	#endregion
	
	#region BUBBLE
	void BubbleEnter() {
		
	}
	
	void BubbleUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED")
		{
			stateMachine.SwitchStates(clockState);
		}
	}
	
	void BubbleExit() 
	{
		BlendHandler.Instance.Blend(machine, clockMachine, 3.0f);
		SetMachine(clockMachine);
		//machineObject = Instantiate(clockMachine.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
	}
	#endregion
}
