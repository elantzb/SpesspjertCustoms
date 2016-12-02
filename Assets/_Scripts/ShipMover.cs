using UnityEngine;
using System.Collections;

using TzFoundation;

public class ShipMover : MonoBehaviour 
{
	public Vector3 initialPos, waitingPos, exitPos;

	public enum MoverState { WAITING_L, ENTERING, WAITING_M, LEAVING, RESETTING };

	public MoverState mState;

	private Coroutine currentLerp;

	void Start () 
	{
		mState = MoverState.WAITING_L;
	}

	void Update () 
	{
		switch(mState)
		{
		case MoverState.WAITING_L:
			break;

		case MoverState.ENTERING:
			if(isWaitingAtDock())
				mState = MoverState.WAITING_M;
			break;

		case MoverState.WAITING_M:
			StopCoroutine(currentLerp);
			break;

		case MoverState.LEAVING:
			if(needsReset())
				mState = MoverState.RESETTING;
			break;

		case MoverState.RESETTING:
			Debug.Log ("Resetting ship pos");
			StopCoroutine(currentLerp);
			JumpTo(initialPos);
			mState = MoverState.WAITING_L;
			break;
		}
	}

	public bool isWaitingToEnter()
	{
		return (gameObject.transform.position - initialPos).magnitude <= 0.01;
	}

	public void StartEntering()
	{
		mState = MoverState.ENTERING;
		currentLerp = StartCoroutine(ProLerps.lerpPositionEaseOut(transform, initialPos, waitingPos, 2.5f));
	}

	public bool isWaitingAtDock()
	{
		return (gameObject.transform.position - waitingPos).magnitude <= 0.01;
	}

	public void StartLeaving()
	{
		mState = MoverState.LEAVING;
		currentLerp = StartCoroutine(ProLerps.lerpPositionEaseIn(transform, waitingPos, exitPos, 2.5f));
	}

	public bool needsReset()
	{
		return (gameObject.transform.position - exitPos).magnitude <= 0.01;
	}

	private void JumpTo(Vector3 dest)
	{
		gameObject.transform.position = dest;
	}
}
