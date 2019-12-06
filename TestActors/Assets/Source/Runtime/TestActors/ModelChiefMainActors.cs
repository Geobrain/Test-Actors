using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//using MoreLinq;

sealed partial class Model
{
    public static void ChiefMainActors(in ent chief)
    {

            var allNumberObj = DataGame.Use.numberObjInScene;
            
            int num = 0;
            do
            {
                Vector2 randPos = new Vector2(Random.Range(0f, 640f), Random.Range(0f, 1280));
                
                var tile = Entity.Create("ModelPoint", Model.Point, randPos);
                ref var cMoveBezier = ref tile.Set<ComponentMoveBezier>();
                var posEnd = new Vector2(Random.Range(0f + 5f, 640f - 5f), Random.Range(0f + 5f, 1280 - 5f));
                var timeToFinish = Random.Range(600, 600);
                
                cMoveBezier.timeToFinish = timeToFinish;
                cMoveBezier.posToMove = new float2x3
                {
                    c0 = new float2(tile.transform.position.x, tile.transform.position.y),
                    c1 = new float2(posEnd.x, posEnd.y),
                    c2 = default
                };
                cMoveBezier.observedDistance = 0;

                
            num++;

            } while (num < allNumberObj);
    }


}

