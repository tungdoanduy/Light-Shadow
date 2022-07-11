using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] GameObject[] button;
    [Header("Wall")]
    GameObject wall;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;
    [SerializeField] float step;
    float runningValue;
    // Start is called before the first frame update
    void Start()
    {
        wall = gameObject;
        runningValue = 0;
        wall.transform.position = Vector3.Lerp(minPos, maxPos, runningValue);
    }

    // Update is called once per frame
    void Update()
    { 
            for (int i=0;i<button.Length;i++)
            {
                if (button[i].GetComponent<WallButton>().PlayerIn )
                {
                    if(runningValue <= 1 - step) runningValue += step;
                    break;
                }
                if (i==button.Length-1&&runningValue>=step)
                {
                    runningValue -= step;
                }
            }
        wall.transform.position = Vector3.Lerp(minPos, maxPos, runningValue);
    }
}
