using System;

// ReSharper disable once CheckNamespace
public class PlaygroundController
{
	private readonly StageControllerBase[] _stages;
	private int _current;

	public IGenerator Generator { get; }
	public PlaygroundRepository Repository { get; }
	public ViewController View { get; }
	public bool IsInWaitState => _stages[_current] is StageControllerWaitForRound;

	public PlaygroundController(IExecutor executor, PlaygroundRepository repository, ViewController view)
	{
		Generator = new OptionGeneratorCheating(repository);
		Repository = repository;
		View = view;
		_stages = new StageControllerBase[]
		{
			new StageControllerOwnSelect(executor, this),
			new StageControllerFoeSelect(executor, this),
			new StageControllerRoundResult(executor, this),
			new StageControllerCounterChange(executor, this),
			new StageControllerWaitForRound(executor, this),
		};
		_current = Array.FindIndex(_stages, _ => _ is StageControllerWaitForRound);
	}

	public void SetWaitStage()
	{
		_stages[_current].Stop();
		_current = Array.FindIndex(_stages, _ => _ is StageControllerWaitForRound);
		_stages[_current].Start();
	}

	public void SetNextStage()
	{
		_stages[_current].Stop();
		_current = ++_current % _stages.Length;
		_stages[_current].Start();
	}

	public void Release() { }
}