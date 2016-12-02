using UnityEngine;
using System.Collections.Generic;

public class CargoType
{
	public string name;
	public bool isContraband;

	private static List<CargoType> cargoTypes = new List<CargoType>();

	public CargoType(string p_name, bool p_isContraband)
	{
		this.name = p_name;
		this.isContraband = p_isContraband;
	}

	public static List<CargoType> GetTypes()
	{
		if(cargoTypes.Count == 0)
		{
			cargoTypes.Add (new CargoType("Fresh Produce", false));
			cargoTypes.Add (new CargoType("Iron Filings", false));
			cargoTypes.Add (new CargoType("Extremely Illegal Drugs", true));
			cargoTypes.Add (new CargoType("Space Curios", false));
			cargoTypes.Add (new CargoType("Human Flesh", true));
			cargoTypes.Add (new CargoType("Toe Jam", false));
			cargoTypes.Add (new CargoType("Crying Children", true));
			cargoTypes.Add (new CargoType("Moon Rocks", false));
			cargoTypes.Add (new CargoType("Death Sticks", true));
			cargoTypes.Add (new CargoType("Helium Canisters", false));
			cargoTypes.Add (new CargoType("Innocuous-Looking Boxes", false));
			cargoTypes.Add (new CargoType("Yellow Cake Uranium", true));
			cargoTypes.Add (new CargoType("Miniature Black Holes", true));
			cargoTypes.Add (new CargoType("Siamese Spacecats", false));
			cargoTypes.Add (new CargoType("Arabica Coffee", false));
			cargoTypes.Add (new CargoType("Rib Eye Steaks", false));
			cargoTypes.Add (new CargoType("Marijuana Needles", false));
			cargoTypes.Add (new CargoType("Political Propaganda", true));
			cargoTypes.Add (new CargoType("Hitler Figurines", true));
		}

		return cargoTypes;
	}
}
