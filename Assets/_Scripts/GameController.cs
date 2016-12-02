using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

using TzFoundation;

public class GameController : SerializableSingleton<GameController> 
{
	private List<ISpessEvent> eventQueue = new List<ISpessEvent>();
	public ISpessEvent currentEvent;

	private enum SpessGameState { INIT, IDLE, READY, EVENT_STARTING, EVENT_RUNNING, EVENT_ENDING };
	private SpessGameState gameState;

	private bool gameOver = false;
		

	void Start () 
	{
		gameState = SpessGameState.INIT;

		eventQueue.Add (new TutorialEvent());
		for(int i = 0; i < 5; i++)
		{
			eventQueue.Add (new ShipEvent());
		}
	}
	

	void Update () 
	{
		switch(gameState)
		{
		case SpessGameState.INIT:
			gameState = SpessGameState.IDLE;
			break;

		case SpessGameState.IDLE:
			gameState = SpessGameState.READY;
			break;

		case SpessGameState.READY:
			if(HasEventQueued() && StartNextEvent())
				gameState = SpessGameState.EVENT_STARTING;
			else if(!HasEventQueued() && !gameOver)
			{
				gameOver = true;
				UIBoss.Instance.setTextConsoleText("You have completed your directives for the day and will receive 100 RAMs.\nGood job.");
				UIBoss.Instance.AddActionButton ("Quit");
			}
			break;

		case SpessGameState.EVENT_STARTING:
			gameState = SpessGameState.EVENT_RUNNING;
			break;

		case SpessGameState.EVENT_RUNNING:
			if(RunCurrentEvent())
				gameState = SpessGameState.EVENT_ENDING;
			break;

		case SpessGameState.EVENT_ENDING:
			if(EndCurrentEvent())
				gameState = SpessGameState.IDLE;
			break;

		default:
			break;
		}
	}


	bool HasEventQueued()
	{
		return eventQueue.Count > 0;
	}

	bool StartNextEvent()
	{
		Debug.Log ("Starting next event.");
		if(eventQueue.Count > 0)
		{
			currentEvent = eventQueue[0];
			eventQueue.RemoveAt(0);
			return currentEvent.eventStart ();
		}

		return false;
	}

	bool RunCurrentEvent()
	{
		return currentEvent.eventRun ();
	}

	bool EndCurrentEvent()
	{
		return currentEvent.eventEnd ();
	}

	//

	public ISpessEvent GetCurrentEvent()
	{
		return currentEvent;
	}
}
