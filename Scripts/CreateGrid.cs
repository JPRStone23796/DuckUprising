using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreateGrid : MonoBehaviour {

    public GameObject block1;
    GameManager GM;
    LevelClass LC;
    [Range(10, 50)]
    public int GridXHeight = 10;
    [Range(10, 50)]
    public int GridZHeight  = 10;
    GameObject[,] Grid;
    LevelManager LM;

 

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        LC = GameObject.Find("GameManager").GetComponent<LevelClass>();
        LM = GetComponent<LevelManager>();
        SetLevel();
        LM.GameStart();
       
    }


    void Update()
    {
        if(transform.position != Vector3.zero)
        {
            MoveToDestination();
        }
        else
        {
            LM.GameBegin();
        }
    }


    Vector3 Target;
    float speed = 4f;
    private float startTime;
    private float journeyLength;

    void SetDestination()
    {
        Target = new Vector3(0, 0, 0);
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, Target);
    }
    void MoveToDestination()
    {

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, Target, fracJourney);
        if (Vector3.Distance(transform.position, Target) < 0.001)
        {
            transform.position = Vector3.zero;
        }
    }
    
    public GameObject Columns, Walker,Player,Turret, Sniper;
    void GridCreation()
    {
        int i = 0;
        int ID = 0;
        Grid = new GameObject[GridXHeight, GridZHeight];
        LM.SetEnemeies();
        for (int x = 0; x < GridXHeight; x += 1)
        {
          
            for (int z = 0; z < GridZHeight; z += 1)
            {

                int currentPiece = int.Parse(Level[x].Substring(z, 1));
                if (Level[x].Substring(z, 1) != "0")
                {
                    i++;            
                    Grid[x, z] = Instantiate(block1);
                    Grid[x, z].transform.position = new Vector3(this.transform.position.x + x /*- (0.02f * x)*/ , this.transform.position.y, this.transform.position.z + z /*- (0.02f * z)*/);
                    Grid[x, z].transform.parent = transform.gameObject.transform;
                    Grid[x, z].name = x.ToString("00") + z.ToString("00");
                }

                else
                {
                    Grid[x, z] = null;
                }

                if (Level[x].Substring(z, 1) == "2")
                {
                    var temp = Instantiate(Columns, new Vector3(Grid[x, z].transform.position.x, Grid[x, z].transform.position.y + +(Grid[x, z].transform.lossyScale.y / 2), Grid[x, z].transform.position.z), Quaternion.identity);
                    temp.name = "Column";
                    temp.transform.parent = transform;
                    Grid[x, z].GetComponent<GridSquare>().SetAbove(true);
                }


                if (currentPiece >= 4  && currentPiece < 8)
                {
                    var temp = Instantiate(Walker, new Vector3(Grid[x, z].transform.position.x, Grid[x, z].transform.position.y + (Grid[x, z].transform.lossyScale.y/2), Grid[x, z].transform.position.z), Quaternion.identity);
                    temp.name = "Walker";
                    temp.transform.parent = transform;
                    int currentDir = currentPiece - 4;
                    switch (currentDir)
                    {
                        case 0: temp.GetComponent<PathWalkerAI>().currentDir = new Vector2(1, 0); break;
                        case 1: temp.GetComponent<PathWalkerAI>().currentDir = new Vector2(-1, 0); break;
                        case 2: temp.GetComponent<PathWalkerAI>().currentDir = new Vector2(0, 1); break;
                        case 3: temp.GetComponent<PathWalkerAI>().currentDir = new Vector2(0, -1); break;
                    }
                    IEnemy enemy = (IEnemy)temp.GetComponent(typeof(IEnemy));
                    enemy.SetPos(new Vector2(x, z));
                    enemy.SetRotation(currentDir);
                    enemy.SetLM(GetComponent<LevelManager>());
                    if (temp.GetComponent<PathFinding>()!=null)
                    {
                      temp.GetComponent<PathFinding>().CurrentGridPosition = new Vector2(x, z);
                    }
                   
                    Grid[x, z].GetComponent<GridSquare>().SetAbove(true);
                    EnemyID newID = new EnemyID(ID,temp);
                    enemy.SetID(newID);
                    LM.AddEnemy(newID);
                    ID++;
                    
                  
                }


                if (currentPiece >= 8 && currentPiece < 12)
                {
                    var temp = Instantiate(Turret, new Vector3(Grid[x, z].transform.position.x, Grid[x, z].transform.position.y + (Grid[x, z].transform.lossyScale.y / 2), Grid[x, z].transform.position.z), Quaternion.identity);
                    temp.name = "Turret";
                    temp.transform.parent = transform;
                    int currentDir = currentPiece - 4;

                    IEnemy enemy = (IEnemy)temp.GetComponent(typeof(IEnemy));
                    enemy.SetPos(new Vector2(x, z));
                    enemy.SetRotation(currentDir);
                    enemy.SetLM(GetComponent<LevelManager>());
                    if (temp.GetComponent<PathFinding>() != null)
                    {
                        temp.GetComponent<PathFinding>().CurrentGridPosition = new Vector2(x, z);
                    }

                    Grid[x, z].GetComponent<GridSquare>().SetAbove(true);
                    EnemyID newID = new EnemyID(ID, temp);
                    enemy.SetID(newID);
                    LM.AddEnemy(newID);
                    ID++;


                }

                if (currentPiece >= 12 && currentPiece < 16)
                {
                    var temp = Instantiate(Sniper, new Vector3(Grid[x, z].transform.position.x, Grid[x, z].transform.position.y + (Grid[x, z].transform.lossyScale.y / 2), Grid[x, z].transform.position.z), Quaternion.identity);
                    temp.name = "Sniper";
                    temp.transform.parent = transform;
                    int currentDir = currentPiece - 4;

                    IEnemy enemy = (IEnemy)temp.GetComponent(typeof(IEnemy));
                    enemy.SetPos(new Vector2(x, z));
                    enemy.SetRotation(currentDir);
                    enemy.SetLM(GetComponent<LevelManager>());
                    if (temp.GetComponent<PathFinding>() != null)
                    {
                        temp.GetComponent<PathFinding>().CurrentGridPosition = new Vector2(x, z);
                    }

                    Grid[x, z].GetComponent<GridSquare>().SetAbove(true);
                    EnemyID newID = new EnemyID(ID, temp);
                    enemy.SetID(newID);
                    LM.AddEnemy(newID);
                    ID++;


                }
                if (Level[x].Substring(z, 1) == "3")
                {
                    var temp = Instantiate(Player, new Vector3(Grid[x, z].transform.position.x, Grid[x, z].transform.position.y + 1, Grid[x, z].transform.position.z), Quaternion.identity);
                    temp.name = "Player";
                    temp.transform.parent = transform;
                    PlayerMovement player = temp.GetComponent<PlayerMovement>();
                    player.CurrentSquare = new Vector2(x, z);
                    Grid[x, z].GetComponent<GridSquare>().SetAbove(true);
                    LM.setPlayer(temp);
                }
            }
        }
        SetDestination();
    }





    string[] Level;
    void SetLevel()
    {
        Level = new string[10];
        int CurrentLevel = GM.Stats.ReturnCurrentLevel()-1;
        for(int i=0;i<Level.Length;i++)
        {

            switch (i)
            {
                case 0: Level[i] = LC.Level[CurrentLevel,i]; break;
                case 1: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 2: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 3: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 4: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 5: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 6: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 7: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 8: Level[i] = LC.Level[CurrentLevel, i]; break;
                case 9: Level[i] = LC.Level[CurrentLevel, i]; break;
            }
        }
        GridCreation();
    }
   

    public Vector2 GridSized()
    {
        return new Vector2(GridXHeight, GridZHeight);
    }
    public GameObject[,] SettingGrid()
    {
        return Grid;
    }
}

[System.Serializable]
public class Prefabs
{
    public GameObject Prefab;
    public Color colour;

   
}