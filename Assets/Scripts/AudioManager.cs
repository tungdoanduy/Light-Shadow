using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        if (music.Length > 1) Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
