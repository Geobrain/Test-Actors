using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;

public class ComponentMoveBezier
{
	public float2x3 posToMove;
	
	public float distanceFull;
	public float velocityToOneSecond;
	
	public float observedDistance;
	public float timeToFinish;
}


#region UTILITIES

public static partial class UtilsComponent
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Enable(this ComponentMoveBezier component, in Vector3 posStart, in Vector3 posEnd, in float timeToFinish, in Action callBack = null)
	{
		component.timeToFinish = timeToFinish;
	    
		component.posToMove = new float2x3
		{
			c0 = new float2(posStart.x, posStart.y),
			c1 = new float2(posEnd.x, posEnd.y),
			c2 = default
		};

		component.observedDistance = 0;
	}
	
}

#endregion


#region HELPERS

static partial class component
{
	public const string MoveBezier = "ComponentMoveBezier";

	public static ref ComponentMoveBezier ComponentMoveBezier(in this ent entity)
		=> ref StorageComponentMoveBezier.components[entity.id];
}

sealed class StorageComponentMoveBezier : Storage<ComponentMoveBezier>
{
	public override ComponentMoveBezier Create() => new ComponentMoveBezier();
}

#endregion


