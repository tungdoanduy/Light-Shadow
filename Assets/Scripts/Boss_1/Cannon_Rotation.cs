using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Rotation : MonoBehaviour
{
    GameObject cannon;
    float timeStartRotation;
    bool isActive = false;
    float timeTransparency;
    [SerializeField] int nextRotation;
    [SerializeField] int currentRotation;
    [SerializeField] int minRotation;
    [SerializeField] int maxRotation;
    void Start()
    {
        cannon = gameObject;
        timeStartRotation = 0;
        cannon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
        nextRotation = Random.Range(minRotation, maxRotation + 1);
    }

    // Update is called once per frame
    void Update()
    {
        cannon.transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, 0, currentRotation)), Quaternion.Euler(new Vector3(0, 0, nextRotation)), (Time.time - timeStartRotation)/7);
        if(Time.time>=timeStartRotation+8)
        {
            timeStartRotation = Time.time;
            currentRotation = nextRotation;
            nextRotation = Random.Range(minRotation, maxRotation+1);
        }
        if (cannon.activeInHierarchy&&!isActive)
        { 
            isActive = true;
            timeTransparency = Time.time;
        }
        if (isActive) cannon.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (Time.time - timeTransparency) / 2);
    }
}
