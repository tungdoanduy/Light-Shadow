using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;
    GameObject movingPlatform;
    float runningValue;
    [SerializeField] float step;
    // Start is called before the first frame update
    void Start()
    {
        movingPlatform = gameObject;
        runningValue = 0;
        movingPlatform.transform.position = Vector3.Lerp(minPos, maxPos, runningValue);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runningValue > 1||runningValue<0) step = 0 - step;
        if (gameObject.CompareTag("Item") && step < 0) step = 0; //just for item dropped after boss die
        runningValue += step;
        movingPlatform.transform.position = Vector3.Lerp(minPos, maxPos, runningValue);
    }
}
