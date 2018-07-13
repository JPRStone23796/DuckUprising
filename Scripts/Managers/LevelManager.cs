using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    enum Phases { GameStart, PlayerAction, EnemyAction, PlayerDeath, LevelTransition }
    Phases currentPhase;
     PlayerMovement player;
    Text GameOver;
    

    List<EnemyID> Enemies;
    public void GameStart()
    {
        GameOver = GameObject.Find("UIOverlay").GetComponent<Text>();
        currentPhase = Phases.GameStart;
        StartCoroutine(LevelLoop());
    }

    public void setPlayer(GameObject playerObj)
    {
        player = playerObj.GetComponent<PlayerMovement>();
    }

    public void SetEnemeies()
    {
        Enemies = new List<EnemyID>();
      
    }
    bool Playerdead;
    IEnumerator LevelLoop()
    {
        while (currentPhase !=Phases.PlayerDeath)
        {
            if (currentPhase == Phases.GameStart && bbeginGame)
            {
                GameOver.text = "";
                currentPhase = Phases.PlayerAction;
            }
            else if (currentPhase == Phases.PlayerAction)
            {
                player.ResetAction();
                bool ActionFinished = false;

                while (ActionFinished == false)
                {
                    ActionFinished = player.Movement();
                   
                    yield return null;
                }
                currentPhase = Phases.EnemyAction;
                       
                
            }

            else if (currentPhase == Phases.EnemyAction && Enemies.Count > 0)
            {
             
                for (int i = 0; i < Enemies.Count; i++)
                {
                    IEnemy enemy = (IEnemy)Enemies[i].EnemyObj.GetComponent(typeof(IEnemy));
                    enemy.ResetAction();
                }

                int EnemyActionsCompleted = 0;
                while(EnemyActionsCompleted!=Enemies.Count)
                {
                    EnemyActionsCompleted = 0;
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        IEnemy enemy = (IEnemy)Enemies[i].EnemyObj.GetComponent(typeof(IEnemy));
                        bool actionCompleted = enemy.TakeAction();
                        if(actionCompleted)
                        {
                            EnemyActionsCompleted++;
                        }
                    }
                    yield return null;
                }

                if(EnemyActionsCompleted==Enemies.Count)
                {
                    currentPhase = Phases.PlayerAction;
                }
             



            }
            else if(Enemies.Count<=0)
            {               
                currentPhase = Phases.LevelTransition;
             

            }
            
            if(currentPhase==Phases.LevelTransition)
            {
                yield return StartCoroutine(NextLevel());
            }         
                yield return null;
        }
        

        while(currentPhase==Phases.PlayerDeath)
        {
           GameOver.text = "Tap Screen to Restart";
            if (Input.GetButtonDown("Fire1"))
            {
                Playerdead = false;
                GameOver.text = "";
                 GameObject.Find("GameManager").GetComponent<GameManager>().ReloadLevel();

            }
            yield return null;
        }

        yield return null;
    }

    float timer = 3f;

    IEnumerator NextLevel()
    {

        GameOver.text = "Level Completed";
        while (timer>0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

 
            int countdown = 4;
            while(countdown>1)
            {
                countdown--;
                GameOver.text = "Starting Next Level in " + countdown.ToString();
                yield return new WaitForSeconds(1.0f);
            }

        GameOver.text = "";
        GameObject.Find("GameManager").GetComponent<GameManager>().NextLevel();



        yield return null;
    }





    bool bbeginGame = false;
    public void GameBegin()
    {
        if(bbeginGame==false)
        {
            bbeginGame = true;
        }
    }



  

    public void PlayerDeath()
    {
        currentPhase = Phases.PlayerDeath;
    }


    public void AddEnemy(EnemyID newEnemy)
    {
        Enemies.Add(newEnemy);
    }

    public void RemoveEnemy(EnemyID newEnemy)
    {
       if(Enemies.Contains(newEnemy))
        {
            Enemies.Remove(newEnemy);
        }
    }



}

public struct EnemyID
{
    int ID;
    public GameObject EnemyObj;

    public EnemyID(int idNumber, GameObject enemy)
    {
        ID = idNumber;
        EnemyObj = enemy;
    }
}
