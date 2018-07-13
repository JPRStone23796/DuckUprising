using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWalkerAI : MonoBehaviour, IEnemy
{
    EnemyClass CurrentEnemy;
    GameManager GM;
    public Vector2 currentDir;
    int gridXSize = 0;
    int gridYSize = 0;
    PlayerMovement player;
    GameObject GridParent;
    Rigidbody rb;
    bool actionFinished;
    bool currentlyMovement = false;

    private void Start()
    {

        GridParent = GameObject.Find("Grid");
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        var GridSize = GridParent.GetComponent<CreateGrid>().GridSized();
        gridXSize = (int)GridSize.x;
        gridYSize = (int)GridSize.y;
        CurrentEnemy.Grid = new GameObject[gridXSize, gridYSize];
        CurrentEnemy.Grid = GridParent.GetComponent<CreateGrid>().SettingGrid();
        //currentDir = new Vector2(1, 0);
    }

    public void ResetAction()
    {
        actionFinished = false;
        currentlyMovement = false;
    }


    public void SetPos(Vector2 startPos)
    {
        CurrentEnemy = new EnemyClass();
        CurrentEnemy.UpdatePosition(startPos);
    }

    public void SetID(EnemyID id)
    {
        CurrentEnemy.SetID(id);
    }

    public void SetLM(LevelManager LM)
    {
        CurrentEnemy.SetLevelManager(LM);
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
    public void RemoveEnemy()
    {
        CurrentEnemy.RetreiveLevelManager().RemoveEnemy(CurrentEnemy.RetreiveID());
    }



    public bool TakeAction()
    {

        if(currentlyMovement==false)
        {
            bool bMovementFound = false;
            bool bPlayerFound = false;

            Vector2 nextPos = Vector2.zero;
            int xPos = 0;
            int yPos = 0;
            while (bMovementFound == false)
            {
                nextPos = CurrentEnemy.RetreivePosition() + currentDir;
                xPos = (int)nextPos.x;
                yPos = (int)nextPos.y;


                if (((currentDir.y == -1 && yPos >= 0) || (currentDir.y == 1 && yPos < gridYSize) || currentDir.y == 0) && ((currentDir.x == -1 && xPos >= 0) || (currentDir.x == 1 && xPos < gridXSize) || currentDir.x == 0))
                {
                    if (CurrentEnemy.Grid[xPos, yPos] == null || CurrentEnemy.Grid[xPos, yPos].GetComponent<GridSquare>().ObjectAbove())
                    {
                        currentDir *= -1;
                    }
                    else if (CurrentEnemy.Grid[xPos, yPos].GetComponent<GridSquare>().ObjectAbove() == false)
                    {
                        bMovementFound = true;
                    }
                }
                else
                {
                    currentDir *= -1;
                }

                if (player.CurrentSquare == new Vector2(xPos, yPos))
                {
                    bPlayerFound = true;
                }

            }

            if (bPlayerFound)
            {
                Vector3 targetDir = player.transform.position - transform.position;
                rb.velocity = targetDir.normalized * 35;
                actionFinished = true;
                CurrentEnemy.RetreiveLevelManager().PlayerDeath();
            }
            else
            {
                Vector2 GridDifference = CurrentEnemy.RetreivePosition() - nextPos;
                if (GridDifference.x == -1) { transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0)); }
                else if (GridDifference.x == 1) { transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); }
                else if (GridDifference.y == -1) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); }
                else if (GridDifference.y == 1) { transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); }


                CurrentEnemy.Grid[(int)CurrentEnemy.RetreivePosition().x, (int)CurrentEnemy.RetreivePosition().y].GetComponent<GridSquare>().SetAbove(false);
                CurrentEnemy.Grid[xPos, yPos].GetComponent<GridSquare>().SetAbove(true);
                CurrentEnemy.UpdatePosition(new Vector2(xPos, yPos));

                Vector3 targetPosition = CurrentEnemy.Grid[xPos, yPos].transform.position;
                targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);



                StartCoroutine(MovePos(transform, targetPosition, 0.2f));
            }

        }

        return actionFinished;
    }



    IEnumerator MovePos(Transform transform,Vector3 endPos, float movement)
    {
        currentlyMovement = true;
        var t = 0f;
        var currentPos = transform.position;
        while(t<1)
        {
            t += Time.deltaTime / movement;
            transform.position = Vector3.Lerp(currentPos, endPos, t);
            yield return null;
        }
        actionFinished = true;

    }


    
}
