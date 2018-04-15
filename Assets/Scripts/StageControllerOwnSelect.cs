using System.Collections;

// ReSharper disable once CheckNamespace
public class StageControllerOwnSelect : StageControllerBase
{
	public StageControllerOwnSelect(IExecutor executor, PlaygroundController controller) : base(executor, controller) { }

	protected override IEnumerator Execute()
	{
		Controller.View.LogMessage("OWN selection: " + Controller.Repository.OwnOption.Name);
		yield return Controller.View.PerformSwitchOwn();
	}
}