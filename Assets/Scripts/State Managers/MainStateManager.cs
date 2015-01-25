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
			machine.EnableShapes();
			machine.EnableLight();
			machine.gameObject.SetActive(true);

			BlendHandler.Instance.Background.renderer.material.color = machine.BackgroundColor;

			Switch(bubbleState, bubble, 1.0f);

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

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		machine.Execute();

		if (Input.GetKeyDown(KeyCode.Space))
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
			Switch(bicycleState, sex, 1.0f);
		}
	}
	
	void IceCreamExit() {}
	#endregion

	#region SEX
	void SexEnter() {}
	
	void SexUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED" || Input.GetKeyDown(KeyCode.Space))
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
}
