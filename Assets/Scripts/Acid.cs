using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] GameObject[] button;
    Animator anim;
    float timeStart;
    bool buttonOn = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < button.Length; i++)
        {
            if (button[i].GetComponent<WallButton>().PlayerIn)
            {
                if (buttonOn)
                {
                    buttonOn = false;
                    timeStart = Time.time;
                    anim.Play("EndAcid");
                }
                if (Time.time >= timeStart + 0.4f) anim.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                break;
            }
            if (i == button.Length - 1)
            {
                if (!buttonOn)
                {
                    anim.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    buttonOn = true;
                    timeStart = Time.time;
                    anim.Play("StartAcid");
                }
                if (Time.time >= timeStart + 0.4f) anim.Play("LoopAcid");
            }
        }
    }
}
