using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAI : MonoBehaviour,IEnemy
{
    [SerializeField] GameObject bullet;
    EnemyClass CurrentEnemy;
    bool actionFinished;

    public void SetID(EnemyID id)
    {
        CurrentEnemy.SetID(id);
        actionFinished = false;
    }



    public void SetPos(Vector2 startPos)
    {
        CurrentEnemy = new EnemyClass();
        CurrentEnemy.UpdatePosition(startPos);
    }

    public void ResetAction()
    {
        actionFinished = false;
    }

    public void SetRotation(int pos)
    {
        switch (pos)
        {
            case 0: transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); break;
            case 1: transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); break;
            case 2: transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); break;
            case 3: transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0)); break;
        }


    }

    public void SetLM(LevelManager LM)
    {
        CurrentEnemy.SetLevelManager(LM);
    }

    public void RemoveEnemy()
    {
        CurrentEnemy.RetreiveLevelManager().RemoveEnemy(CurrentEnemy.RetreiveID());
    }

    public bool TakeAction()
    {    
            RaycastHit hit;
            Ray sniperFire = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(sniperFire, out hit, 100f))
            {         
                if (hit.transform.tag == "Player")
                {
                   
                    GameObject bulletFired = Instantiate(bullet, transform.position, Quaternion.identity);
                    bulletFired.GetComponent<Rigidbody>().velocity = transform.forward.normalized * 20;
                      CurrentEnemy.RetreiveLevelManager().PlayerDeath();
                 }
            }
            else
            {
            actionFinished = true;
            }
        
        return actionFinished;
    }

  

}
