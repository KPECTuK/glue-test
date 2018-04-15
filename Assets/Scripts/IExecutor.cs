using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
public interface IExecutor
{
	Coroutine StartCoroutine(IEnumerator enumerator);
	void StopCoroutine(Coroutine coroutine);
}