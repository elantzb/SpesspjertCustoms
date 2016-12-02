using UnityEngine;
using System.Collections;

public class Cargo
{
	public CargoType type;
	public int amount;

	public Cargo(CargoType p_cargoType, int p_cargoAmount)
	{
		this.type = p_cargoType;
		this.amount = p_cargoAmount;
	}

	public bool IsContraband()
	{
		return type.isContraband;
	}
}
