using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateManager : MonoBehaviour
{
	public bool initialized;

	public SimpleStateMachine stateMachine;
	SimpleState setupState, menuState, connectState, playState, disconnectState, finishedState;

	void Start()
	{
		Init();
	}

	public void Init () 
	{
		setupState = new SimpleState(SetupEnter, SetupUpdate, SetupExit, "SETUP");
		menuState = new SimpleState(MenuEnter, MenuUpdate, MenuExit, "MENU");
		connectState = new SimpleState(ConnectEnter, ConnectUpdate, ConnectExit, "CONNECT");
		playState = new SimpleState(PlayEnter, PlayUpdate, PlayExit, "PLAY");
		disconnectState = new SimpleState(DisconnectEnter, DisconnectUpdate, DisconnectExit, "DISCONNECT");
		finishedState = new SimpleState(null, null, null, "FINISHED");

		Setup();
	}
	void Update () 
	{
		Execute();
	}

	public void Setup () 
	{
		stateMachine.SwitchStates(setupState);
	}

	public void Connect ()
	{
		stateMachine.SwitchStates(connectState);
	}

	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region SETUP
	void SetupEnter() 
	{

	}

	void SetupUpdate() 
	{
		stateMachine.SwitchStates(connectState);
	}

	void SetupExit() {}
	#endregion

	#region MENU
	void MenuEnter() {}

	void MenuUpdate() {}

	void MenuExit() {}
	#endregion

	#region CONNECT
	void ConnectEnter() {}

	void ConnectUpdate() 
	{

	}

	void ConnectExit() 
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
		stateMachine.SwitchStates(menuState);
	}
	#endregion
}
