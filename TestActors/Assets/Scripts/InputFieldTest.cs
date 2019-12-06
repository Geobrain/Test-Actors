using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{
    //public TMP_InputField inputField;
    public DataGame dataGame;
    public Text text;

    public void SetNumObj()
    {
        dataGame.numberObjInScene = int.Parse(text.text);
    }
    
    

}
