using System.Collections;

// ReSharper disable once CheckNamespace
public class StageControllerFoeSelect : StageControllerBase
{
	public StageControllerFoeSelect(IExecutor executor, PlaygroundController controller) : base(executor, controller) { }

	protected override IEnumerator Execute()
	{
		Controller.Repository.SetFoeOption(Controller.Generator.GetOption());
		Controller.View.LogMessage("FOE selection: " + Controller.Repository.FoeOption.Name);
		return Controller.View.PerformSwitchFoe();
	}
}