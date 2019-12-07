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


public class ProcessorMoveBezier_job2 : Processor, ITick
{
	
	public float sidePosMultMin =  0.9f;
	public float sidePosMultMax = 1.1f;
	
	Group<ComponentMoveBezier> groupMoveBezier;
	
    TransformAccessArray transformsAccessArray;
	Transform[] transforms;
	JobPositionUpdate jobPositionUpdate;

	private NativeArray<Obj> nObj;
	private NativeArray<float> nDist;
	private NativeArray<float> nDistFull;
	private NativeArray<float> nVel;
	private NativeArray<float2x3> nPosToMove;
	private NativeArray<float2> nNewPos;

	int index;
	protected override void OnDispose()
	{
		transformsAccessArray.Dispose();

		nDist.Dispose();
		nDistFull.Dispose();
		nVel.Dispose();
		nPosToMove.Dispose();
	}
	
	
	public ProcessorMoveBezier_job2()
	{
		transforms = new Transform[DataGame.Use.numberObjInScene+1];
		transformsAccessArray = new TransformAccessArray(DataGame.Use.numberObjInScene+1);

		nObj = new NativeArray<Obj>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
		nDist = new NativeArray<float>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
		nDistFull = new NativeArray<float>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
		nVel = new NativeArray<float>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
		nPosToMove = new NativeArray<float2x3>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
		nNewPos = new NativeArray<float2>(DataGame.Use.numberObjInScene+1, Allocator.Persistent);
	}


	public override void HandleEvents()
	{
		Debug.Log("ProcessorMoveBezier_job2");
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

			nDist[index] = 0;
			nDistFull[index] = cMoveBezier.distanceFull;
			nVel[index] = cMoveBezier.velocityToOneSecond;
			nPosToMove[index] = cMoveBezier.posToMove;
			
			
			ref var obj = ref cObject.obj;
			nObj[index] = obj;
			
			index++;
		}
		
		transformsAccessArray.SetTransforms(transforms);
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

#if UNITY_EDITOR
		for (var i = 0; i < index; i++)
		{
			var obj = nObj[i];
			DebugDrowBox(obj.collBox, Color.blue, Time.deltaTime);
		}
#endif

		var distUpJobHandle = new JobDistUpdate
		{
			deltaTime = delta,
			nDist = nDist,
			nVel = nVel
		}.Schedule(DataGame.Use.numberObjInScene, 1024);

		var besierJobHandle = new JobBesierUpdate
		{
			nDist = nDist,
			nDistFull = nDistFull,
			nNewPos = nNewPos,
			nPosToMove = nPosToMove
		}.Schedule(DataGame.Use.numberObjInScene, 1024, distUpJobHandle);

		var applyPosHandle = new JobApplyPosUpdate
		{
			nNewPos = nNewPos
		}.Schedule(transformsAccessArray, besierJobHandle);

		var colliderUpdateJob = new JobColliderUpdate
		{
			nObj = nObj,
			nNewPos = nNewPos
		}.Schedule(DataGame.Use.numberObjInScene, 1024, besierJobHandle);

		applyPosHandle.Complete();
		colliderUpdateJob.Complete();
	}
}

[BurstCompile]
struct JobDistUpdate : IJobParallelFor
{
	public NativeArray<float> nDist;
	[Unity.Collections.ReadOnly] public NativeArray<float> nVel;
	[Unity.Collections.ReadOnly] public float deltaTime;

	public void Execute(int index)
	{
		nDist[index] += nVel[index] * deltaTime;
	}
}

[BurstCompile]
struct JobBesierUpdate : IJobParallelFor
{
	[Unity.Collections.ReadOnly] public NativeArray<float> nDist;
	[Unity.Collections.ReadOnly] public NativeArray<float> nDistFull;
	[Unity.Collections.ReadOnly] public NativeArray<float2x3> nPosToMove;
	public NativeArray<float2> nNewPos;
	public void Execute(int index)
	{
		var t = math.min(1.0f, nDist[index] / nDistFull[index]);
		nNewPos[index] = t.CalculateBesierPos(nPosToMove[index].c0, nPosToMove[index].c2,nPosToMove[index].c1);
	}
}

[BurstCompile]
struct JobApplyPosUpdate : IJobParallelForTransform
{
	[Unity.Collections.ReadOnly] public NativeArray<float2> nNewPos;
	public void Execute(int index, TransformAccess transform)
	{
		transform.localPosition = (Vector2)nNewPos[index];
	}
}

[BurstCompile]
struct JobColliderUpdate : IJobParallelFor
{
	[Unity.Collections.ReadOnly] public NativeArray<float2> nNewPos;
	public NativeArray<Obj> nObj;
	public void Execute(int index)
	{
		var newPos = nNewPos[index];

		var obj = nObj[index];
		obj.properties.c0 = newPos;
		var posAndSize = new float2x2
		{
			c0 = newPos,
			c1 = obj.collBox.posAndSize.c1
		};
		obj.collBox = obj.entity.NewCollBox(posAndSize, new float2(10f, 10f), obj.rotation.ToEulerAnglesZ());
		nObj[index] = obj;
	}
}


