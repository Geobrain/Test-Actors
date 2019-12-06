using System;
using System.Runtime.CompilerServices;
using Common;
using Pixeye.Actors;
using UnityEngine;


public sealed class ComponentChief
{

	
}

#region HELPERS

static partial class component
	{
		public const string Chief = "ComponentChief";
		public static ref ComponentChief ComponentChief(in this ent entity) => ref StorageComponentChief.components[entity.id];
	}

	sealed class StorageComponentChief : Storage<ComponentChief>
	{
		public override ComponentChief Create() => new ComponentChief();
	}

#endregion

