using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{
    //public TMP_InputField inputField;
    public DataGame dataGame;
    public Text text;

    private void Start()
    {
        dataGame.numberObjInScene = 0;
    }

    public void SetNumObj()
    {
        dataGame.numberObjInScene = int.Parse(text.text);
    }
    
    

}
