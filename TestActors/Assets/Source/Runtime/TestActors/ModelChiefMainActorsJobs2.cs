using Common;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//using MoreLinq;

sealed partial class Model
{
    public static void ChiefMainActorsJobs2(in ent chief)
    {

        var allNumberObj = DataGame.Use.numberObjInScene;

        GameObject goParent = new GameObject();
        goParent.name = "Pack" + 0;
        
        int num = 0;
        
        int numParent = 0;
        int numObjChild = 0;
        do
        {
            Vector2 randPos = new Vector2(Random.Range(0f, 640f), Random.Range(0f, 1280));

            if (numObjChild > 255)
            {
                numParent++;
                goParent = new GameObject();
                goParent.name = "Pack" + numParent;
                numObjChild = 0;
            }
            
            
            var tile = Entity.Create("ModelPoint", Model.Point, randPos);
            var cMoveBezier = tile.Set<ComponentMoveBezier>();
            var posEnd = new Vector2(Random.Range(0f + 5f, 640f - 5f), Random.Range(0f + 5f, 1280 - 5f));
            var timeToFinish = Random.Range(600, 600);
            cMoveBezier.Enable( tile.transform.position, posEnd, timeToFinish, () =>{});
            tile.transform.parent = goParent.transform;
            

        num++;
        numObjChild++;

        } while (num < allNumberObj);
    }



}
