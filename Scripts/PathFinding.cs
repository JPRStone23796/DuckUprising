using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour
{
    GameObject GridParent,Player;
    GameObject[,] Grid;
    Vector2 Goal, CurrentGrid;
    public Vector2 CurrentGridPosition;
    List<CalculatePath> Open = new List<CalculatePath>();
    List<CalculatePath> Closed = new List<CalculatePath>();
    Queue<Vector2> Path = new Queue<Vector2>();
    int currentPos;
    Vector2[] AIpath;
    Vector2 CurrentParent;
    AIMovement MainMovement;
    public bool pathfound;
    int PathEnd;

    void Start()
    {
        GridParent = GameObject.Find("Grid");
        MainMovement = GetComponent<AIMovement>();
        var GridSize = GridParent.GetComponent<CreateGrid>().GridSized();
        Grid = new GameObject[(int)GridSize.x, (int)GridSize.y];
        Grid = GridParent.GetComponent<CreateGrid>().SettingGrid();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FindingPath()
    {
            Goal = Player.GetComponent<PlayerMovement>().CurrentSquare;
            Open.Add(new CalculatePath(CurrentGridPosition, Vector2.zero));
            AIMovement();
            FindPath();
            MainMovement.GetPath(AIpath, AIpath.Length);
            resetValues();                  
    }

    void AIMovement()
    {
        while (pathfound == false)
        {
            CalculatePoint();
            CalculateNeighbours();
        }
    }

    void CalculatePoint()
    {
        float Fcost = 100;
        for (int i = 0; i < Open.Count; i++)
        {
            var CurrentFCost = CalculateCost(Open[i].ReturningPosition(), CurrentGridPosition) + CalculateCost(Open[i].ReturningPosition(), Goal);
            if (CurrentFCost < Fcost)
            {
                Fcost = CurrentFCost;
                CurrentGrid = Open[i].ReturningPosition();
                currentPos = i;
            }
        }
    }

    float CalculateCost(Vector2 GridPosition, Vector2 StartPosition)
    {
        var x = Mathf.Abs(GridPosition.x - StartPosition.x);
        var y = Mathf.Abs(GridPosition.y - StartPosition.y);
        return x + y;
    }

    void CalculateNeighbours()
    {

        NeighbourCheck(CurrentGrid.x, 9, new Vector2(CurrentGrid.x + 1, CurrentGrid.y), false);
        NeighbourCheck(CurrentGrid.x, 0, new Vector2(CurrentGrid.x - 1, CurrentGrid.y), true);
        NeighbourCheck(CurrentGrid.y, 9, new Vector2(CurrentGrid.x, CurrentGrid.y + 1), false);
        NeighbourCheck(CurrentGrid.y, 0, new Vector2(CurrentGrid.x, CurrentGrid.y - 1), true);

        Closed.Add(Open[currentPos]);
        if (CalculateCost(Open[currentPos].ReturningPosition(), Goal) <= 1)
        {

            PathEnd = Closed.Count - 1;
            pathfound = true;

        }
        Open.Remove(Open[currentPos]);

    }

    void NeighbourCheck(float check, float value, Vector2 pos, bool GreaterThan)
    {

        if ((check > value && GreaterThan) || (check < value && GreaterThan == false))
        {
            GameObject ThisGrid = Grid[(int)pos.x, (int)pos.y];

            if (ThisGrid != null)
            {
                if ((ThisGrid.GetComponent<GridSquare>().ObjectAbove() == false ) )
                {
                    var current = new CalculatePath(pos, CurrentGrid);

                    if (CheckList(pos, Closed) == false && CheckList(pos, Open) == false)
                    {

                        Open.Add(current);
                    }

                }
            }
        }
    }

    bool CheckList(Vector2 pos, List<CalculatePath> path)
    {
        bool exist = false;
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].ReturningPosition() == pos)
            {
                exist = true;
                break;
            }
        }
        return exist;
    }

    void FindPath()
    {
        Path.Enqueue(Closed[PathEnd].ReturningPosition());
        CurrentParent = Closed[PathEnd].ReturningParent();

        while (CurrentParent != Vector2.zero)
        {
            var currentPosition = ParentSearch(CurrentParent);

            if (Closed[currentPosition].ReturningParent() == Vector2.zero) { break; }
            Path.Enqueue(Closed[currentPosition].ReturningPosition());
            CurrentParent = Closed[currentPosition].ReturningParent();


        }

        int PathSize = Path.Count;
        AIpath = new Vector2[PathSize];
        for (int i = 0; i < PathSize; i++)
        {
            AIpath[i] = Path.Dequeue();

        }
        Path.Clear();
    }

    int ParentSearch(Vector2 Parent)
    {
        int pos = 0;
        for (int i = 0; i < Closed.Count; i++)
        {
            if (Closed[i].ReturningPosition() == Parent)
            {
                pos = i;
                break;
            }
        }

        return pos;
    }

    void resetValues()
    {
        AIpath = null;
        Open.Clear();
        Closed.Clear();
        pathfound = false;
    }

    public Vector2 returnGoal()
    {
        return Goal;
    }
        
}


class CalculatePath
{
    Vector2 GridPosition,ParentPos;


    public CalculatePath(Vector2 Position,Vector2 Parent)
    {
        GridPosition = Position;
        ParentPos = Parent;
    }

    public Vector2 ReturningPosition()
    {
        return GridPosition;
    }

    public Vector2 ReturningParent()
    {
        return ParentPos;
    }
}