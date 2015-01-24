using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(PolygonRenderer))]
public class ObjectStateManager : MonoBehaviour
{
	public bool initialized;

	public SimpleStateMachine stateMachine;
	SimpleState circleState, clockState, squareState, playState, disconnectState, finishedState;

	PolygonRenderer poly;

	void Start()
	{
		Init();
	}

	public void Init () 
	{
		poly = gameObject.GetComponent<PolygonRenderer> ();

		circleState = new SimpleState(CircleEnter, CircleUpdate, CircleExit, "CIRCLE");
		clockState = new SimpleState(ClockEnter, ClockUpdate, ClockExit, "CLOCK");
		squareState = new SimpleState(SquareEnter, SquareUpdate, SquareExit, "SQUARE");

		/*
		playState = new SimpleState(PlayEnter, PlayUpdate, PlayExit, "PLAY");
		disconnectState = new SimpleState(DisconnectEnter, DisconnectUpdate, DisconnectExit, "DISCONNECT");
		finishedState = new SimpleState(null, null, null, "FINISHED");
		*/

		Circle();
	}
	void Update () 
	{
		Execute();
	}

	public void Circle () 
	{
		stateMachine.SwitchStates(circleState);
	}

	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region circle
	void CircleEnter() 
	{

	}

	void CircleUpdate() 
	{

	}

	void CircleExit() {}
	#endregion

	#region clock
	void ClockEnter() {}

	void ClockUpdate() {}

	void ClockExit() {}
	#endregion

	#region square
	void SquareEnter() {


	}

	void SquareUpdate() 
	{

	}

	void SquareExit() 
	{

	}
	#endregion

	#region PLAY
	void PlayEnter()
	{
		
	}

	void PlayUpdate() 
	{
		
	}

	void PlayExit() {}
	#endregion

	#region DISCONNECT
	void DisconnectEnter() {}

	void DisconnectUpdate() {}

	void DisconnectExit() 
	{
		stateMachine.SwitchStates(clockState);
	}
	#endregion
}
