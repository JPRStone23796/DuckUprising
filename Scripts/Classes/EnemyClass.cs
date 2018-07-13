using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass
{
 
    Vector2 CurrentPosition;
    public GameObject[,] Grid;
    EnemyID myID;
    LevelManager LM;
    public EnemyClass()
    { 
        CurrentPosition = Vector2.zero;
        Grid = null;
        myID = new EnemyID();
        LM = null;
    }

    public Vector2 RetreivePosition()
    {
        return CurrentPosition;
    }

    public void UpdatePosition(Vector2 positon)
    {
        CurrentPosition = positon;
    }


    public void SetID(EnemyID id)
    {
        myID = id;
    }

    public EnemyID RetreiveID()
    {
        return myID;
    }


    public void SetLevelManager(LevelManager lm)
    {
        LM = lm;
    }

    
    public LevelManager RetreiveLevelManager()
    {
        return LM;
    }






}
