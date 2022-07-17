using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] CanvasGroup[] canvasGroups;
    int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameData data =SaveSystem.LoadGame();
        level = data.Level;
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == "Level " + (i + 1).ToString() && i + 1 > level)
            {
                level = i + 1;
                break;
            }
        }
        for (int i=0;i<level;i++)
        {
            canvasGroups[i].interactable = true;
            canvasGroups[i].alpha = 1;
        }
        for (int i = level;i<canvasGroups.Length;i++)
        {
            canvasGroups[i].interactable = false;
            canvasGroups[i].alpha = 0;
        }
    }
}
