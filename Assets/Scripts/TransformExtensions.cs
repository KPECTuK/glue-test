using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class TransformExtensions
{
	public static IEnumerable<TComponent> FindOnAllChildren<TComponent>(this Transform branch, Func<TComponent, bool> filter = null, bool searchHidden = true)
		where TComponent : Component
	{
		var children = branch.GetComponentsInChildren<TComponent>(searchHidden);
		return filter == null
			? children
			: children.Where(filter);
	}
}