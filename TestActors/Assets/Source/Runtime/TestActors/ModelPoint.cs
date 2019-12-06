using Common;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;
using Rand = Common.Rand;

sealed partial class Model
{
    public static void Point(in ent entity)
    {
        var cObject = entity.Set<ComponentObject>();
        cObject.SetCache(entity);
    }


    
}

