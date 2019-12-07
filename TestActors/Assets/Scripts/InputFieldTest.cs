using System;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{
    //public TMP_InputField inputField;
    //public DataGame dataGame;
    public Text text;

    private void Start()
    {
        //dataGame.numberObjInScene = 0;
        DataGame.Use = (DataGame) Pixeye.Actors.Box.Load<ScriptableObject>("DataGame");
        DataGame.Use.numberObjInScene = 1;
    }

    public void SetNumObj()
    {
        //Timer.Add(1f, () => { dataGame.numberObjInScene = int.Parse(text.text); });
        DataGame.Use.numberObjInScene = int.Parse(text.text);
    }
    
    

}
