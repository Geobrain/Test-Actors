using System.Collections.Generic;
using UnityEngine;
using Pixeye.Actors;
//using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "DataGame", menuName = "Actors/Add/DataGame")]
public class DataGame : ScriptableObject, IKernel
{
    public static DataGame Use;
    //public static GameSession Default => Toolbox.Get<GameSession>();
    //public static GameSession Use => ScriptableObject.CreateInstance<GameSession>();
    //public static GameSession Use = new GameSession();
    //public static GameSession Use => Toolbox.Get<GameSession>();
    public int numberObjInScene;
}
