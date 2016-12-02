using System;
using UnityEngine;

namespace TzFoundation
{
	public abstract class SerializableSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get 
			{
				if(_instance == null)
				{
					GameObject singleton = new GameObject();
					_instance = singleton.AddComponent<T>();
					singleton.name = "(singleton) " + typeof(T).ToString ();

					DontDestroyOnLoad(singleton);
				}

				return _instance;
			}
		}

		public void Awake()
		{
			if(_instance != null)
				GameObject.Destroy (_instance);
			else
			{
				_instance = gameObject.GetComponent<T>();
				gameObject.name = "(singleton) " + typeof(T).ToString ();

				DontDestroyOnLoad(gameObject);
			}
		}
	}
}

