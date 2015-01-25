using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;
	SimpleState clockState, bubbleState, icecreamState, sexState, bicycleState;
	SimpleState finishedState;

	public GameObject machineObject;
	public FrameStateManager machine;
	public GameObject icecream;
	public GameObject bubble;
	public GameObject clock;
	public GameObject sex;
	public GameObject bicycle;

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
			machine.EnableAll();

			BlendHandler.Instance.Background.renderer.material.color = machine.BackgroundColor;

			Switch(bubbleState, bubble, 3.0f);

			this.initialized = true;
		}
	}

	public void Switch(SimpleState nextState, GameObject nextObject, float duration)
	{
		stateMachine.SwitchStates(nextState);
		Destroy(machineObject, duration + 0.1f);
		machineObject = Instantiate(nextObject, Vector3.zero, Quaternion.identity) as GameObject;
		FrameStateManager nextMachine = machineObject.GetComponent<FrameStateManager>();
		nextMachine.DisableAll();
		BlendHandler.Instance.Blend(machine, nextMachine, duration);

		machine = nextMachine;
		machineObject = nextMachine.gameObject;
		StartCoroutine(EnableAfterDelay(machine, 3.0f));
	}

		IEnumerator EnableAfterDelay(FrameStateManager machine, float duration)
		{
			Timer timer = new Timer(duration);
			while (timer.Percent() < 1f)
			{
				yield return 0;
			}

			machine.EnableAll();
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
	void IceCreamEnter() {}
	
	void IceCreamUpdate() 
	{
		machine.Execute();
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Switch(sexState, sex, 3.0f);
		}
	}
	
	void IceCreamExit() {}
	#endregion

	#region SEX
	void SexEnter() {}
	
	void SexUpdate() 
	{
		machine.Execute();
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Switch(bicycleState, bicycle, 3.0f);
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
}
