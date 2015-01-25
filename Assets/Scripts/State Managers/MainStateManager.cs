using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;
	SimpleState clockState, bubbleState, icecreamState;
	SimpleState finishedState;

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
		machine = bubbleMachine;
		machine.enabled = true;
		stateMachine.SwitchStates(bubbleState);
	}

	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		clockMachine.Execute();
	}

	void ClockExit() {}
	#endregion
	
	#region ICECREAM
	void IceCreamEnter() {
	
	}
	
	void IceCreamUpdate() 
	{
		icecreamMachine.Execute();
		
		if (icecreamMachine.stateMachine.currentState == "FINISHED")
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
		bubbleMachine.Execute();
		
		if (bubbleMachine.stateMachine.currentState == "FINISHED")
		{
			stateMachine.SwitchStates(clockState);
		}
	}
	
	void BubbleExit() 
	{
		BlendHandler.Instance.Blend(machine, clockMachine, 3.0f);
	}
	#endregion
}
