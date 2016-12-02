using UnityEngine;
using System.Collections;

public interface ISpessEvent 
{
	bool eventStart();

	bool eventRun();

	bool eventEnd();

	bool eventNotify(string eventMsg);
}
