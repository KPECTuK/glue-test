using System.Collections;

// ReSharper disable once CheckNamespace
public class StageControllerRoundResult : StageControllerBase
{
	public StageControllerRoundResult(IExecutor executor, PlaygroundController controller) : base(executor, controller) { }

	protected override IEnumerator Execute()
	{
		if(Controller.Repository.IsOwnLoss())
		{
			Controller.View.LogMessage("-- FOE Win");
			Controller.Repository.IncreaseFoeCounter();
			return Controller.View.PerformLoss();
		}

		if(Controller.Repository.IsOwnWin())
		{
			Controller.View.LogMessage("-- OWN Win");
			Controller.Repository.IncreaseOwnCounter();
			return Controller.View.PerformWin();
		}

		Controller.View.LogMessage("-- Draw");
		return Controller.View.PerformDraw();
	}
}