using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateManager : MonoBehaviour
{
	public SimpleStateMachine stateMachine;
	SimpleState clockState, bubbleState, icecreamState, sexState, bicycleState, pawState, startScreenState, pigState, spaceState, caterpillarState, cloudState, heartState;
	SimpleState finishedState;

	public GameObject machineObject;
	public FrameStateManager machine;
	public GameObject icecream, bubble, clock, sex, bicycle, paw, startScreen, pig, space, caterpillar, cloud, heart;

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
		spaceState = new SimpleState(SpaceEnter, SpaceUpdate, SpaceExit, "SPACE");
		caterpillarState = new SimpleState(CaterpillarEnter, CaterpillarUpdate, CaterpillarExit, "CATERPILLAR");
		cloudState = new SimpleState(CloudEnter, CloudUpdate, CloudExit, "CLOUD");
		heartState = new SimpleState(HeartEnter, HeartUpdate, HeartExit, "HEART");

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
	Timer delayTimer;
	void BubbleEnter() 
	{
		delayTimer = new Timer(1.0f);	
	}
	
	void BubbleUpdate() 
	{
		Debug.Log("asdfs");
		if (delayTimer.Percent() >= 1f)
		{
			machine.Execute();
			
			if (machine.stateMachine.currentState == "FINISHED_BUBBLE")
			{
				Switch(clockState, clock, 1.0f);
			}
		}
	}
	
	void BubbleExit() {}
	#endregion
	
	#region STARTSCREEN
	void startScreenEnter() {
		
	}
	
	void startScreenUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_STARTSCREEN")
		{
			Switch(bubbleState, bubble, 2.0f);
		}
	}
	
	void startScreenExit() {}
	#endregion

	#region CLOCK
	void ClockEnter() {}

	void ClockUpdate() 
	{
		machine.Execute();

		if (machine.stateMachine.currentState == "FINISHED_CLOCK")
		{
			Switch(sexState, sex, 1.0f);
		} 
		else if (machine.stateMachine.currentState == "FINISHED_CLOCK2")
		{
			Switch(sexState, sex, 1.0f);
		}
	}

	void ClockExit() {}
	#endregion
	
	#region ICECREAM
	void IceCreamEnter() {}
	
	void IceCreamUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_ICECREAM" || Input.GetKeyDown(KeyCode.Space))
		{
			Switch(pawState, paw, 1.0f);
		}
	}
	
	void IceCreamExit() {}
	#endregion

	#region SEX
	void SexEnter() {}
	
	void SexUpdate() 
	{
		machine.Execute();

		if (machine.stateMachine.currentState == "FINISHED_SEX1" || Input.GetKeyDown(KeyCode.Space))
		{
			Switch(icecreamState, icecream, 1.0f);
		}
		if (machine.stateMachine.currentState == "FINISHED_SEX2")
		{
			Switch(bicycleState, bicycle, 1.0f);
		}
	}
	
	void SexExit() {}
	#endregion

	#region PIG
	void PigEnter() {}
	void PigUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_PIG")
		{
			
		}
	}
	void PigExit() {}
	#endregion

	#region SPACE
	void SpaceEnter() {}
	void SpaceUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_SPACE")
		{
			Switch(heartState, heart, 2.0f);
		}
	}
	void SpaceExit() {}
	#endregion

	#region CATERPILLAR
	void CaterpillarEnter() {}
	void CaterpillarUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_CATERPILLAR")
		{
			Switch(cloudState, cloud, 2.0f);
		}
	}
	void CaterpillarExit() {}
	#endregion

	#region CLOUD
	void CloudEnter() {}
	void CloudUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_CLOUDS")
		{
			Switch(pawState, paw, 2.0f);
		}
	}
	void CloudExit() {}
	#endregion

	#region HEART
	void HeartEnter() {}
	void HeartUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_HEART")
		{
			Switch(bubbleState, bubble, 1.0f);
		}
	}
	void HeartExit() {}
	#endregion

	#region BICYCLE
	void BicycleEnter() {}
	
	void BicycleUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_BICYCLE")
		{
			Switch(caterpillarState, caterpillar, 2.0f);
		}
	}
	
	void BicycleExit() {}
	#endregion

	#region PAW
	void PawEnter() {}
	
	void PawUpdate() 
	{
		machine.Execute();
		
		if (machine.stateMachine.currentState == "FINISHED_PAW")
		{
			Switch(spaceState, space, 2.0f);
		}
	}
	
	void PawExit() {}
	#endregion
}
