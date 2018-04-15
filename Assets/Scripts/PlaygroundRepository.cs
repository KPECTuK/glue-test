using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlaygroundRepository
{
	private class Option : IOption
	{
		public string Name { get; set; }
		public Sprite Sprite { get; set; }
		//
		public IOption WillLoseOf { get; set; }
		public IOption WillWinOf { get; set; }
		public IOption WillDrawOf { get; set; }

		// prefer Release(), should be Dispose { } pattern
		public void Dispose()
		{
			// cant be called outside main thread
			// GameObject.Destroy(Sprite);
			Sprite = null;
		}
	}
	
	private IOption[] _options;

	public IOption OwnOption { get; private set; }
	public IOption FoeOption { get; private set; }

	public int OwnCounter { get; private set; }
	public int FoeCounter { get; private set; }

	public void IncreaseOwnCounter()
	{
		OwnCounter++;
	}

	public void IncreaseFoeCounter()
	{
		FoeCounter++;
	}

	public void SetOwnOption(IOption option)
	{
		OwnOption = option;
	}

	public void SetFoeOption(IOption option)
	{
		FoeOption = option;
	}

	public bool IsOwnWin()
	{
		return ReferenceEquals(OwnOption, FoeOption.WillLoseOf);
	}

	public bool IsOwnLoss()
	{
		return ReferenceEquals(OwnOption, FoeOption.WillWinOf);
	}

	public PlaygroundRepository()
	{
		var asset = Resources.LoadAll("ui");
		var stone = new Option
		{
			Name = "Stone", 
			Sprite = asset.OfType<Sprite>().SingleOrDefault(_ => _.name.Contains("stone")),
		};
		var scissors = new Option
		{
			Name = "Scissors",
			Sprite = asset.OfType<Sprite>().SingleOrDefault(_ => _.name.Contains("scissors")),
		};
		var paper = new Option
		{
			Name = "Paper",
			Sprite = asset.OfType<Sprite>().SingleOrDefault(_ => _.name.Contains("paper")),
		};
		Resources.UnloadUnusedAssets();
		//
		stone.WillLoseOf = paper;
		stone.WillWinOf = scissors;
		stone.WillDrawOf = stone;
		//
		scissors.WillLoseOf = stone;
		scissors.WillWinOf = paper;
		scissors.WillDrawOf = scissors;
		//
		paper.WillLoseOf = scissors;
		paper.WillWinOf = stone;
		paper.WillDrawOf = paper;
		//
		_options = new IOption[] { stone, scissors, paper };
	}

	public IEnumerable<IOption> GetAllOptions()
	{
		return _options;
	}

	public void Release()
	{
		Array.ForEach(_options, _ => _.Dispose());
		_options = null;
		Resources.UnloadUnusedAssets();
	}
}