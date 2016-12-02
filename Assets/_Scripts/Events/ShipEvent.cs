using UnityEngine;
using System.Collections.Generic;

public class ShipEvent : ISpessEvent 
{
	private UIBoss uiBoss;

	public List<Cargo> shipCargo = new List<Cargo>();

	string introText = "A ship has arrived and is awaiting inspection.";

	string scanText = "You scanned the ship's cargo and it contains:\n";

	string seizeText = "You order an immediate seizure of the ship's illegal goods. ";

	string[] seizeSuffixes = {
		"They cry as you end their shipping career.",
		"They curse your M.O.T.H.E.R as they fly off.",
		"Your payment will process as soon as the bank opens.",
		"Pat yourself on the back.",
		"A small child is discovered in the storage compartment. Space CPS has been notified.",
		"All the RAMs to you.",
		"+1000 RAMs.",
		"You are getting so good at this.",
		"Your parents have been notified of your stellar performance."
	};

	string waveThroughText = "You wave them through.";

	private bool arrived = false;
	private bool complete = false;
	private bool seized = false;


	public bool eventStart()
	{
		uiBoss = UIBoss.Instance;

		int numCargoTypes = Mathf.CeilToInt (Random.value * 5);
		int cargoAmount = 0, cargoTypeIndex = 0;
		CargoType cargoType;
		for(int i = 0; i < numCargoTypes; i++)
		{
			cargoAmount = Mathf.CeilToInt (Random.value * 12) * 10;
			cargoTypeIndex = Mathf.FloorToInt (Random.value * CargoType.GetTypes ().Count);
			cargoType = CargoType.GetTypes()[cargoTypeIndex];
			if(!ContainsCargoType(cargoType))
				shipCargo.Add (new Cargo(cargoType, cargoAmount));
			else
				i--;
		}

		uiBoss.GetShipMover().StartEntering ();
		uiBoss.setTextConsoleText (introText);

		return true;
	}
	
	public bool eventRun()
	{
		if(!arrived && uiBoss.GetShipMover ().isWaitingAtDock ())
		{
			Debug.Log ("arrival.");
			arrived = true;
			uiBoss.AddActionButton("Wave Through");
			uiBoss.AddActionButton("Scan Cargo");
			uiBoss.ArrangeActionButtons ();
		}
		else if(arrived && uiBoss.GetShipMover ().isWaitingToEnter() && !seized)
		{
			Debug.Log ("Event complete.");
			complete = true;
		}

		return complete;
	}
	
	public bool eventEnd()
	{
		Debug.Log ("Ship Event ending.");
		return true;
	}
	
	public bool eventNotify(string eventMsg)
	{
		if(eventMsg == "Scan Cargo")
		{
			string scanMsg = scanText;
			for(int i = 0; i < shipCargo.Count; i++)
			{
				scanMsg += shipCargo[i].amount + " x " + shipCargo[i].type.name + ((shipCargo[i].type.isContraband) ? " (Illegal)" : "");
				scanMsg += ((i % 2 != 0) ? "\n" : "\t\t");
			}
			uiBoss.setTextConsoleText (scanMsg);

			if(ContainsContraband())
			{
				uiBoss.AddActionButton ("Seize Contraband");
				uiBoss.ArrangeActionButtons();
			}
		}

		if(eventMsg == "Seize Contraband")
		{
			string seizeBlurb = seizeText;
			seizeBlurb += seizeSuffixes[Mathf.FloorToInt (Random.value * seizeSuffixes.Length)];

			uiBoss.setTextConsoleText (seizeBlurb);
			seized = true;

			uiBoss.GetShipMover ().StartLeaving ();
			uiBoss.ClearActionButtons();
			uiBoss.AddActionButton ("Acknowledge");
		}

		if(eventMsg == "Wave Through")
		{
			uiBoss.GetShipMover ().StartLeaving ();
			uiBoss.ClearActionButtons();

			string endText = waveThroughText;
			if(ContainsContraband())
			{
				endText += "\nIt is unfortunate that you let them smuggle illegal goods through the SpesspJert. -20 RAMs.";
				seized = true; // more cheatery. see cheating comment below.
				uiBoss.AddActionButton ("Acknowledge");
			}
			uiBoss.setTextConsoleText(endText);

			Debug.Log ("arrived/seized/waitingToEnter: " + arrived + "/" + seized + "/" + uiBoss.GetShipMover ().isWaitingToEnter());
		}

		if(eventMsg == "Acknowledge")
		{
			uiBoss.ClearActionButtons();
			seized = false; // cheating here but it gets the job done. See the last case in eventRun().

			Debug.Log ("arrived/seized/waitingToEnter: " + arrived + "/" + seized + "/" + uiBoss.GetShipMover ().isWaitingToEnter());
		}

		return true;
	}

	private bool ContainsContraband()
	{
		bool contrabandFound = false;
		for(int i = 0; i < shipCargo.Count; i++)
		{
			if(shipCargo[i].IsContraband ())
			{
				contrabandFound = true;
				break;
			}
		}

		return contrabandFound;
	}

	private bool ContainsCargoType(CargoType cargoType)
	{
		bool found = false;

		for(int i = 0; i < shipCargo.Count; i++)
		{
			if(shipCargo[i].type.name == cargoType.name)
			{
				found = true;
				break;
			}
		}

		return found;
	}
}
