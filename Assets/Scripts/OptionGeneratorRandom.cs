using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class OptionGeneratorRandom : IGenerator
{
	private readonly PlaygroundRepository _repository;

	public OptionGeneratorRandom(PlaygroundRepository repository)
	{
		_repository = repository;
	}

	public IOption GetOption()
	{
		var total = _repository.GetAllOptions().ToArray();
		return total.Take(Random.Range(1, total.Length)).Last();
	}
}
