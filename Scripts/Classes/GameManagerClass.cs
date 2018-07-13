using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerClass {

    int CurrentLevel;
    public GameManagerClass()
    {
        CurrentLevel = 1;
    }

   
     
    public void NextLevel()
    {
        CurrentLevel++;
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
    }

	

    public int ReturnCurrentLevel()
    {
        return CurrentLevel;
    }

}
