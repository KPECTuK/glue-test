using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public interface IOption : IDisposable
{
	string Name { get; }

	Sprite Sprite { get; }

	IOption WillLoseOf { get; }
	IOption WillWinOf { get; }
	IOption WillDrawOf { get; }
}