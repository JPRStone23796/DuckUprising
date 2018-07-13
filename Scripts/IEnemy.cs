using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    void SetLM(LevelManager lm);
    bool TakeAction();
    void SetID(EnemyID id);
    void SetRotation(int pos);
    void SetPos(Vector2 startPos);
    void ResetAction();
    void RemoveEnemy();

  


}
