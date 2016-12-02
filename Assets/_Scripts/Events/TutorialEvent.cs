using UnityEngine;
using System.Collections;

public class TutorialEvent : ISpessEvent 
{
	private UIBoss uiBoss;
	private bool complete = false;

	private string tutorialText = "You are AI. Very cheap AI. You are licensed to SpessPjert Customs.\n" +
		"Your directives are to wave incoming ships through, if they do not carry contraband.\nBegin.";

	public bool eventStart()
	{
		Debug.Log ("Tutorial Event starting.");
		uiBoss = UIBoss.Instance;
		uiBoss.setTextConsoleText (tutorialText);
		uiBoss.AddActionButton("Acknowledge");
		uiBoss.ArrangeActionButtons ();
		return true;
	}

	public bool eventRun()
	{
		if(complete)
			return true;
		return false;
	}

	public bool eventEnd()
	{
		Debug.Log ("Tutorial Event ending.");
		uiBoss.ClearActionButtons();
		return true;
	}

	public bool eventNotify(string eventMsg)
	{
		Debug.Log ("Tutorial got notif: " + eventMsg);
		if(eventMsg == "Acknowledge")
		{
			complete = true;
		}

		return false;
	}
}
