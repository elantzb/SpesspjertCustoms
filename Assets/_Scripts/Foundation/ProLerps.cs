/*
 * Functions for lerping.
 * 
 * Based on the protips at https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
 * 
*/

using UnityEngine;
using System.Collections;

namespace TzFoundation
{
	public static class ProLerps
	{
		public delegate float FunctionCurve(float t);

		// Function Curves //
		public static float LINEAR(float t)
		{
			return t;
		}

		public static float EASEOUT(float t)
		{
			return Mathf.Sin (t * Mathf.PI * 0.5f);
		}

		public static float EASEIN(float t)
		{
			return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
		}

		public static float EASEINOUT(float t)
		{
			return 0.5f - 0.5f * Mathf.Cos (t * Mathf.PI);
		}

		public static float EASEOUTIN(float t)
		{
			//(θ+sin(θ))/(2 π) | θ = 0 to 2 π
			return (t*2*Mathf.PI + 0.8f*Mathf.Sin (t*2*Mathf.PI))/(2*Mathf.PI);
		}

		// Shortcut Functions // 
		public static IEnumerator lerpPositionLinear(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime)
		{
			return lerpPositionByFunction(ObjectTransform, StartPos, EndPos, TotalLerpTime, LINEAR);
		}
	
		public static IEnumerator lerpPositionEaseOut(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime)
		{
			return lerpPositionByFunction(ObjectTransform, StartPos, EndPos, TotalLerpTime, EASEOUT);
		}
	
		public static IEnumerator lerpPositionEaseIn(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime)
		{
			return lerpPositionByFunction(ObjectTransform, StartPos, EndPos, TotalLerpTime, EASEIN);
		}

		public static IEnumerator lerpPositionEaseInOut(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime)
		{
			return lerpPositionByFunction(ObjectTransform, StartPos, EndPos, TotalLerpTime, EASEINOUT);
		}

		public static IEnumerator lerpPositionEaseOutIn(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime)
		{
			return lerpPositionByFunction(ObjectTransform, StartPos, EndPos, TotalLerpTime, EASEOUTIN);
		}

		// All the work gets done here. //
		public static IEnumerator lerpPositionByFunction(Transform ObjectTransform, Vector3 StartPos, Vector3 EndPos, float TotalLerpTime, FunctionCurve CurveFunction)
		{
			float currentLerpTime = 0f;
			
			while(currentLerpTime < TotalLerpTime)
			{
				currentLerpTime += Time.deltaTime;
				if(currentLerpTime > TotalLerpTime) currentLerpTime = TotalLerpTime;
				float timeProgressed = currentLerpTime / TotalLerpTime;  // this will be 0 at the beginning and 1 at the end.
				timeProgressed = CurveFunction(timeProgressed);
				ObjectTransform.position = Vector3.Lerp(StartPos, EndPos, timeProgressed);
				
				yield return null;
			}
			Debug.Log ("Lerp Complete");
		}
	}
}