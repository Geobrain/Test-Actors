using System;
using UnityEngine;
using Pixeye.Actors;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Common;
using Unity.Burst;
using Unity.Mathematics;
using Rand = Common.Rand;
using Random = UnityEngine.Random;


public class ProcessorMoveBezier_job : Processor, ITick
{
	
	public float sidePosMultMin =  0.9f;
	public float sidePosMultMax = 1.1f;
	
	Group<ComponentMoveBezier> groupMoveBezier;
	
    NativeArray<SetMove> nSetMove = new NativeArray<SetMove>(100000, Allocator.Persistent);
    NativeArray<Obj> nObj = new NativeArray<Obj>(100000, Allocator.Persistent);
    TransformAccessArray transformsAccessArray;
	Transform[] transforms;
	JobPositionUpdate jobPositionUpdate;
	JobHandle handlePositionUpdate;
	
	
	int index;
	protected override void OnDispose()
	{
		nSetMove.Dispose();
		transformsAccessArray.Dispose();
	}
	
	
	public ProcessorMoveBezier_job()
	{
		transforms = new Transform[100000];
		transformsAccessArray = new TransformAccessArray(0);
	}


	public override void HandleEvents()
	{

		foreach (ent entity in groupMoveBezier.added)
		{
			var cObject = entity.ComponentObject();
			transforms[index] = cObject.tr;
			
			ref var cMoveBezier = ref entity.ComponentMoveBezier();
			ref var posToMove = ref cMoveBezier.posToMove;

			cMoveBezier.distanceFull = posToMove.c0.AlphaBetaMagnitude(posToMove.c1);
			
			var sidePosMult = Rand.rnd.NextFloat(sidePosMultMin, sidePosMultMax);
			var nPosSide = posToMove.c0.CenterSidePos(posToMove.c1).PosToRadius(cMoveBezier.distanceFull * 0.25f * sidePosMult);
			cMoveBezier.velocityToOneSecond = cMoveBezier.distanceFull / cMoveBezier.timeToFinish;
			
			cMoveBezier.posToMove.c2 = nPosSide;
			cMoveBezier.observedDistance = 0;
			
			
			SetMove setMove;
			setMove.posToMove = cMoveBezier.posToMove;
			setMove.distanceFull = cMoveBezier.distanceFull;
			setMove.velocityToOneSecond = cMoveBezier.velocityToOneSecond;
			setMove.observedDistance = default;
			nSetMove[index] = setMove;
			
			
			ref var obj = ref cObject.obj;
			nObj[index] = obj;
			
			index++;
		}
	}
	

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DebugDrowBox(in Box box, in Color colorDebug, in float deltaIn)
	{
		Debug.DrawLine((Vector2) box.vertex.c0, (Vector2) box.vertex.c1, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c1, (Vector2) box.vertex.c2, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c2, (Vector2) box.vertex.c3, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c3, (Vector2) box.vertex.c0, colorDebug, Time.deltaTime);
	}
	
	
	public void Tick(float delta)
	{
		if (index <= 0) return;
		
		handlePositionUpdate.Complete();
		
#if UNITY_EDITOR
		for (var i = 0; i < index; i++)
		{
			var obj = nObj[i];
			DebugDrowBox(obj.collBox, Color.blue, Time.deltaTime);
		}
#endif	
		
		jobPositionUpdate.nSetMove = nSetMove;
		jobPositionUpdate.nObj = nObj;
		jobPositionUpdate.deltaTime = delta;
		transformsAccessArray.SetTransforms(transforms);
		handlePositionUpdate = jobPositionUpdate.Schedule(transformsAccessArray);
	}
}


[BurstCompile]
struct JobPositionUpdate : IJobParallelForTransform
{
	public NativeArray<SetMove> nSetMove;
	public NativeArray<Obj> nObj;
	[Unity.Collections.ReadOnly] public float deltaTime;
	
	public void Execute(int index, TransformAccess transform)
	{
		var setMove = nSetMove[index];
		var velocityToOneFrame = nSetMove[index].velocityToOneSecond * deltaTime;
		
		// расчет новой точки
		setMove.observedDistance += velocityToOneFrame;
		var t = setMove.observedDistance / setMove.distanceFull;
		if (t > 1f) t = 1f;
		var newPos = t.CalculateBesierPos(setMove.posToMove.c0, setMove.posToMove.c2,setMove.posToMove.c1);
		nSetMove[index] = setMove;
		
		// Обновление коллайдера
		var obj = nObj[index];
		obj.properties.c0 = newPos;
		var posAndSize = new float2x2
		{
			c0 = newPos,
			c1 = obj.collBox.posAndSize.c1
		};
		obj.collBox = obj.entity.NewCollBox(posAndSize, new float2(10f, 10f), obj.rotation.ToEulerAnglesZ());
		nObj[index] = obj;
		
		// перемещение на новую позицию
		transform.localPosition = (Vector2) newPos;
	}
}


public struct SetMove
{
	public float2x3 posToMove;
	public float distanceFull;
	public float velocityToOneSecond;
	
	public float observedDistance;
}





