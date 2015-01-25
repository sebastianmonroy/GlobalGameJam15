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
	public GameObject icecream;
	public GameObject bubble;
	public GameObject clock;

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
			//SetMachine(bubbleMachine);
			BlendHandler.Instance.Background.renderer.material.color = machine.BackgroundColor;
			Switch(bubbleState, bubble, 3.0f);
			this.initialized = true;
		}
	}

	public void SetMachine(FrameStateManager toMachine)
	{
		machineObject = Instantiate(toMachine.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		machine = machineObject.GetComponent<FrameStateManager>();
		machine.EnableAll();
	}

	public void Switch(SimpleState nextState, GameObject nextObject, float duration)
	{
		stateMachine.SwitchStates(nextState);
		Destroy(machineObject, duration + 0.1f);
		machineObject = Instantiate(nextObject, Vector3.zero, Quaternion.identity) as GameObject;
		FrameStateManager nextMachine = machineObject.GetComponent<FrameStateManager>();
		BlendHandler.Instance.Blend(machine, nextMachine, duration);
		//StartCoroutine(EnableAfterDelay(nextMachine))
		machine = nextMachine;
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
			Switch(clockState, clock, 3.0f);
		}
	}
	
	void BubbleExit() {}
	#endregion

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		machine.Execute();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Switch(icecreamState, icecream, 3.0f);
		}
	}

	void ClockExit() {}
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
}
