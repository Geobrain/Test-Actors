using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// позаимствовал для тестов
public class fps_counter : MonoBehaviour
{
    public TMPro.TMP_Text fps_t;
    Queue<float> values;
    int max = 6;


    private void Awake()
    {
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 0;
    }

    private void Start()
    {
        //Application.targetFrameRate = 0;
        //QualitySettings.vSyncCount = 0;
        
        values = new Queue<float>(max);
    }
    float oldTime =0;
    private void Update()
    {
        values.Enqueue(1 / (Time.realtimeSinceStartup - oldTime));
        if (values.Count > max) values.Dequeue();
        float v = 0;

        foreach (var item in values)
        {
            v += item;
        }
        float fps = v / values.Count;
        oldTime = Time.realtimeSinceStartup;
        fps_t.text = ((int)fps).ToString() + " fps";
    }
}
