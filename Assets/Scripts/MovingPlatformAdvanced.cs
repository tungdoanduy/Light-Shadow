using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAdvanced : MonoBehaviour
{
    GameObject movingPlatform;
    [SerializeField] Vector2[] pos;
    bool[] atPos;
    [SerializeField] float step;
    float runningValue;
    // Start is called before the first frame update
    void Start()
    {
        atPos = new bool[pos.Length];
        movingPlatform = gameObject;
        atPos[0] = true;
        for (int i=1;i<pos.Length;i++)
        {
            atPos[i] = false;
        }
        runningValue = 0;
        movingPlatform.transform.position = Vector3.Lerp(pos[0], pos[1], 0);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<pos.Length;i++)
        {
            if (atPos[i]==true)
            {
                runningValue += step;
                if (i < pos.Length - 1) movingPlatform.transform.position = Vector3.Lerp(pos[i], pos[i+1], runningValue);
                if (i == pos.Length - 1) movingPlatform.transform.position = Vector3.Lerp(pos[i], pos[0], runningValue);
                if (runningValue>=0.99f)
                {
                    atPos[i] = false;
                    if (i < pos.Length - 1) atPos[i + 1] = true;
                    if (i == pos.Length - 1) atPos[0] = true;
                    runningValue = 0;
                }
                break;
            }
        }
    }
}
