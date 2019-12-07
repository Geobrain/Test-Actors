using UnityEngine;

public class TestMono : MonoBehaviour
{
    public GameObject go;

    void Start()
    {
        DataGame.Use = (DataGame) Pixeye.Actors.Box.Load<ScriptableObject>("DataGame");
        //var allNumberObj = DataGame.Use.numberObjInScene == 0 ? 1 : DataGame.Use.numberObjInScene;
        var allNumberObj = DataGame.Use.numberObjInScene;
        
        int num = 0;
        do
        {
            Vector2 randPos = new Vector2(Random.Range(0f, 640f), Random.Range(0f, 1280));
            GameObject.Instantiate(go, randPos, Quaternion.identity);
        num++;
        } while (num < allNumberObj);

    }


}
