using UnityEngine;

public class TestMono : MonoBehaviour
{
    public GameObject go;

    void Start()
    {
        var allNumberObj = 50000;
        
        int num = 0;
        do
        {
            Vector2 randPos = new Vector2(Random.Range(0f, 640f), Random.Range(0f, 1280));
            GameObject.Instantiate(go, randPos, Quaternion.identity);
        num++;
        } while (num < allNumberObj);

    }


}
