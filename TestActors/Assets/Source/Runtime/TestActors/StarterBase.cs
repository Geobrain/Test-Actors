using Pixeye.Actors;
using UnityEngine;
using Random = UnityEngine.Random;
using Time = Pixeye.Actors.time;

namespace Common
{
    public class StarterBase : Starter
    {
        protected override void Setup()
        {
            Rand.rnd = new Unity.Mathematics.Random((uint) Random.Range(1, 10000));
        }


    }
    
    public static class Rand
    {
        public static Unity.Mathematics.Random rnd;
    }

}