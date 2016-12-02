using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using TzFoundation;

public class UIBoss : SerializableSingleton<UIBoss>
{
	[SerializeField]
	public Text textConsole;
	public GameObject actionButtonInstance;
	public Transform ActionButtonContainerT;

	private GameController gameController;


	void Start()
	{
		if(gameController == null)
			gameController = GameController.Instance;
	}
	

	public void AddActionButton(string buttonText)
	{
		Debug.Log ("Adding Action Button: " + buttonText);
		GameObject button = (GameObject)Instantiate(actionButtonInstance, ActionButtonContainerT.transform.position, Quaternion.identity);
		button.GetComponentInChildren<Text>().text = buttonText;
		button.transform.SetParent (ActionButtonContainerT);
	}

	public void ArrangeActionButtons()
	{
		int buttonRowHeight = 32;

		for(int i = 0; i < ActionButtonContainerT.childCount; i++)
		{
			Vector3 pos = ActionButtonContainerT.position;
			pos.y = pos.y + (i * -buttonRowHeight);
			ActionButtonContainerT.GetChild (i).position = pos;
		}
	}

	public void ClearActionButtons()
	{
		Debug.Log ("Clearing Action Buttons.");
		for(int i = 0; i < ActionButtonContainerT.childCount; i++)
		{
			Destroy(ActionButtonContainerT.GetChild (i).gameObject);
		}
	}

	public void setTextConsoleText(string text)
	{
		textConsole.text = text;
	}


	public ShipMover GetShipMover()
	{
		return gameObject.GetComponentInChildren<ShipMover>();
	}

	public void NotifyGameController(string msg)
	{
		ISpessEvent currentEvent = gameController.GetCurrentEvent();
		if(currentEvent != null)
		{
			currentEvent.eventNotify (msg);
		}

		if(msg == "Quit")
		{
			Debug.Log ("Quitting.");
			Application.Quit ();
		}
	}
}
