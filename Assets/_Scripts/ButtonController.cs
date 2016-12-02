using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	public void OnActionButtonClick()
	{
		string buttonText = gameObject.GetComponentInChildren<Text>().text;
		UIBoss.Instance.NotifyGameController(buttonText);
	}
}
