using System.Collections;

// ReSharper disable once CheckNamespace
public class StageControllerCounterChange : StageControllerBase
{
	public StageControllerCounterChange(IExecutor executor, PlaygroundController controller) : base(executor, controller) { }

	protected override IEnumerator Execute()
	{
		return Controller.View.PerformCounterChange();
	}
}