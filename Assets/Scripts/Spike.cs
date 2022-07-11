using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    float timeRunning;
    bool isRunning = true;
    GameObject spike;
    [SerializeField] float timeSpawn;
    [SerializeField] float timeDelay;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;
    
    // Start is called before the first frame update
    void Start()
    {
        spike = gameObject;
        spike.transform.position = Vector3.Lerp(minPos, maxPos,0);
        timeRunning = Time.time+1.9666667f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeRunning + timeSpawn) isRunning = false;
        if (!isRunning)
        {
            isRunning = true;
            timeRunning = Time.time+timeDelay;
        }
        if (isRunning)
        {
            spike.transform.position = Vector3.Lerp(minPos, maxPos, (Time.time-timeRunning)/timeSpawn);
        }
    }
}
