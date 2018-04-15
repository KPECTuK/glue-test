using UnityEngine;

// ReSharper disable once CheckNamespace
public class OptionGeneratorCheating : IGenerator
{
	private readonly PlaygroundRepository _repository;
	private readonly IGenerator _inner;
	private readonly float _probability = .5f;

	public OptionGeneratorCheating(PlaygroundRepository repository)
	{
		_repository = repository;
		_inner = new OptionGeneratorRandom(repository);
	}

	public IOption GetOption()
	{
		var result = _inner.GetOption();
		if(ReferenceEquals(result, _repository.OwnOption))
		{
			return result;
		}
		return Random.value < _probability
			? _repository.OwnOption.WillWinOf
			: _repository.OwnOption.WillLoseOf;
	}
}