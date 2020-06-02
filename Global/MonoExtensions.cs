using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class MonoExtensions
{
	public static Coroutine<T> StartCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine)
	{
		Coroutine<T> coroutineObject = new Coroutine<T>();
		coroutineObject.coroutine = obj.StartCoroutine(coroutineObject.InternalRoutine(coroutine));
		return coroutineObject;
	}
}

public class Coroutine<T>
{
	public T Value 
	{
		get
		{
			if(e != null)
			{
				throw e;
			}
			return returnVal;
		}
	}

	private bool isCancelled = false;
	public T returnVal;
	public Coroutine coroutine;
	private Exception e;

	public IEnumerator InternalRoutine(IEnumerator coroutine)
	{
		while(true){
			// if(isCancelled){
			// 	e = new CoroutineCancelledException();
			// 	yield break;
			// }
			if(!coroutine.MoveNext()){
				yield break;
			}
			object yielded = coroutine.Current;

			if(yielded != null && yielded.GetType() is T)
			{
				returnVal = (T)yielded;
				yield break;
			}
			else
			{
				yield return coroutine.Current;
			}
		}
	}
}
