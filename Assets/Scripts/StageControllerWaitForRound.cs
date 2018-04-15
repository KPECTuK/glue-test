using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class StageControllerWaitForRound : StageControllerBase
{
	public StageControllerWaitForRound(IExecutor executor, PlaygroundController controller) : base(executor, controller) { }

	protected override IEnumerator Execute()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
		}
	}
}