using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.Mathematics;
using Rand = Common.Rand;

//using MoreLinq;

public class ProcessorMoveBezier_Threaded : Processor, ITick
{
  private Group<ComponentMoveBezier> groupMoveBezier;

  public float sidePosMultMin = 0.9f;
  public float sidePosMultMax = 1.1f;

  public ProcessorMoveBezier_Threaded()
  {
    groupMoveBezier.MakeConcurrent(50000, Environment.ProcessorCount - 1, HandleCalculation);
  }

  public override void HandleEvents()
  {
    foreach (ent entity in groupMoveBezier.added)
    {
      ref var cMoveBezier = ref entity.ComponentMoveBezier();
      ref var posToMove   = ref cMoveBezier.posToMove;

      cMoveBezier.distanceFull = posToMove.c0.AlphaBetaMagnitude(posToMove.c1);

      var sidePosMult = Rand.rnd.NextFloat(sidePosMultMin, sidePosMultMax);
      var nPosSide    = posToMove.c0.CenterSidePos(posToMove.c1).PosToRadius(cMoveBezier.distanceFull * 0.25f * sidePosMult);
      cMoveBezier.velocityToOneSecond = cMoveBezier.distanceFull / cMoveBezier.timeToFinish;

      cMoveBezier.posToMove.c2     = nPosSide;
      cMoveBezier.observedDistance = 0;
    }
  }


  public void Tick(float delta)
  {
    groupMoveBezier.Execute(delta);

    for (int i = 0; i < groupMoveBezier.length; i++)
    {
      ref var cObject = ref StorageComponentObject.components[groupMoveBezier.entities[i]];
      cObject.tr.localPosition = new Vector3(cObject.obj.properties.c0.x, cObject.obj.properties.c0.y, 0);
      #if UNITY_EDITOR
      DebugDrowBox(cObject.obj.collBox, Color.blue, Time.deltaTime);
      #endif
    }
  }
  static void HandleCalculation(SegmentGroup segment)
  {
    for (int i = segment.indexFrom; i < segment.indexTo; i++)
    {
      var     id          = segment.source.entities[i].id;
      ref var cMoveBezier = ref StorageComponentMoveBezier.components[id];
      ref var obj         = ref StorageComponentObject.components[id].obj;


      // расчет новой точки
      var t         = (cMoveBezier.observedDistance += cMoveBezier.velocityToOneSecond * segment.delta) / cMoveBezier.distanceFull;
      if (t > 1f) t = 1f;

      // обновление коллайдера
      obj.properties.c0 = (1 - t) * (1 - t) * cMoveBezier.posToMove.c0 + 2 * (1 - t) * t * cMoveBezier.posToMove.c2 + t * t * cMoveBezier.posToMove.c1;

      var posAndSize = new float2x2
      {
        c0 = obj.properties.c0,
        c1 = obj.collBox.posAndSize.c1
      };
      obj.collBox = obj.entity.NewCollBox(posAndSize, new float2(10f, 10f), obj.rotation.ToEulerAnglesZ());
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
}