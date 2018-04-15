using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
public abstract class StageControllerBase
{
	private readonly IExecutor _executor;
	private IEnumerator _outer;
	private Coroutine _coroutine;

	protected readonly PlaygroundController Controller;

	protected StageControllerBase(IExecutor executor, PlaygroundController controller)
	{
		_executor = executor;
		Controller = controller;
	}

	protected abstract IEnumerator Execute();

	private IEnumerator Wrapper()
	{
		_outer = Execute();
		while(_outer.MoveNext())
		{
			yield return _outer.Current;
		}
		Controller.SetNextStage();
	}

	public void Start()
	{
		_coroutine = _executor.StartCoroutine(Wrapper());
	}

	public void Stop()
	{
		if(!ReferenceEquals(null, _coroutine))
		{
			_executor.StopCoroutine(_coroutine);
		}
	}
}