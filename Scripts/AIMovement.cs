using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    GameManager GM;
    GameObject[,] Grid;
    GameObject GridParent;
    public int numberOfTurnsTaken, NumberOfMaxTurns;
    bool PathExists;
    Vector3 Target;
    float speed = 2F;
    private float startTime;
    private float journeyLength;
    Vector2[] currentPath;
    PathFinding pathFinder;
    int Gsize;


  

    void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GridParent = GameObject.Find("Grid");
        pathFinder = GetComponent<PathFinding>();
        NumberOfMaxTurns = 2;
        numberOfTurnsTaken = 0;
        var GridSize = GridParent.GetComponent<CreateGrid>().GridSized();
        Grid = new GameObject[(int)GridSize.x, (int)GridSize.y];
        Grid = GridParent.GetComponent<CreateGrid>().SettingGrid();
    }
	
	void Update () {
        Movement();
	}
 
    void Movement()
    {
  
        //if(GM.Stats.ReturnPlayerTurn()==false)
        //{
        //    if (PathExists == false)
        //    {
        //        pathFinder.FindingPath();
        //        PathExists = true;
        //    }

        //    if(Target==Vector3.zero && numberOfTurnsTaken<NumberOfMaxTurns)
        //    {

        //        SetTarget();               
        //    }
        //    else if (Target != Vector3.zero && numberOfTurnsTaken <NumberOfMaxTurns)
        //    {
        //        SingleTurnMovement();
        //    }
        //    else if (numberOfTurnsTaken==NumberOfMaxTurns)
        //    {
        //        Target = Vector3.zero;
        //        numberOfTurnsTaken = 0;
        //        PathExists = false;
        //        currentPath = null;
        //        GM.Stats.ResetActions();
                
                
        //    }
        //}
    }

    void SetTarget()
    {
        Vector3 Gridpos = Grid[(int)currentPath[(Gsize)-numberOfTurnsTaken].x, (int)currentPath[(Gsize) - numberOfTurnsTaken].y].transform.position;
        var tempGridPos = pathFinder.CurrentGridPosition;

        if (tempGridPos.y == pathFinder.returnGoal().y)
        {
            if (tempGridPos.x  >pathFinder.returnGoal().x)
            {
                if (transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    CheckForObstacles();
                }
            }

            else if (tempGridPos.x < pathFinder.returnGoal().x)
            {
                if (transform.rotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    CheckForObstacles();
                }
            }
        }

        if (tempGridPos.x == pathFinder.returnGoal().x)
        {
            if (tempGridPos.y > pathFinder.returnGoal().y)
            {
                if (transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                {
                    CheckForObstacles();
                }
            }

            else if (tempGridPos.y < pathFinder.returnGoal().y)
            {
                if (transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    CheckForObstacles();
                }
            }
        }
        Target = new Vector3(Gridpos.x, transform.position.y, Gridpos.z);
        startTime = Time.time;
        journeyLength= Vector3.Distance(transform.position, Target);

        Vector2 GridDifference = pathFinder.CurrentGridPosition - currentPath[Gsize - numberOfTurnsTaken];
        if (GridDifference.x == -1) { transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0)); }
        else if (GridDifference.x == 1) { transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); }
        else if (GridDifference.y == -1) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); }
        else if (GridDifference.y == 1) { transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); }

    }

    public GameObject Bullet;
    void CheckForObstacles()
    {
        Debug.DrawRay(transform.position, -transform.forward * 50, Color.red,0.5f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 50))
        {
            var temp = Instantiate(Bullet, transform.position - (transform.forward * 0.5f), Quaternion.identity);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.velocity = (-transform.forward * 10);
            Destroy(temp, 7.0f);
        }
    }
    void SingleTurnMovement()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, Target, fracJourney);
        if (Vector3.Distance(transform.position, Target) < 0.001)
        {
            Target = Vector3.zero;
            pathFinder.CurrentGridPosition = currentPath[Gsize - numberOfTurnsTaken];
            numberOfTurnsTaken++;
        }
    }
   
    public void GetPath(Vector2[] Path, int size)
    {
        currentPath = new Vector2[size];
        currentPath = Path;
        Gsize = size-1;           
    }

}