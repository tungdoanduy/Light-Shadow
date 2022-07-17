using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public GameData(LevelManager levelManager)
    {
        level = levelManager.Level;
    }
    public GameData()
    {
        level = 1;
    }
}
