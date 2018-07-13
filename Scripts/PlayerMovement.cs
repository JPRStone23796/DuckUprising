using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    GameManager GM;
    public AudioClip GunShot;
    private AudioReverbFilter reverb;
    bool ActionTaken = false;
    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        reverb = GetComponent<AudioReverbFilter>();
    }

    void Start()
    {
        gridObject = GameObject.Find("Grid").GetComponent<CreateGrid>();
        GunPos = transform.GetChild(0).transform.GetChild(0).gameObject;
        PlayerAudio = GetComponent<AudioSource>();
        SetGrid();
        SecondPress = Vector2.zero;
    }

  


    GameObject[,] GridSystem;
    public Vector2 CurrentSquare,WorldHeight;
    CreateGrid gridObject;
    void SetGrid()
    {
        WorldHeight = gridObject.GridSized();
        GridSystem = new GameObject[(int)WorldHeight.x,(int) WorldHeight.y];
        GridSystem = gridObject.SettingGrid();
    }


    Vector3 Target = Vector3.zero;
     float speed = 2F;
    private float startTime;
    private float journeyLength;
    bool ActionCompleted = false;

    public void ResetAction()
    {
        ActionCompleted = false;
        ActionTaken = false;
    }
   public bool Movement()
    {

            if (gridObject.transform.position == Vector3.zero)
            {
                if (Target == Vector3.zero && ActionTaken == false)
                {
                //Touch();
                PCTouch();
            }
                else if (Target != Vector3.zero)
                {
                    PositionMovement();
                }
            }
        
      
        return ActionCompleted;
       
    }

    Vector2 FirstPress, SecondPress, CurrentSwipe;
    float TouchHeld;
    void Touch()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if (TouchHeld == 0)
                {
                    FirstPress = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
 
            }

            if(touch.phase == TouchPhase.Moved)
            {
                SecondPress = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if(SecondPress!=Vector2.zero)
                {
                    CurrentSwipe = SecondPress - FirstPress;
                    CurrentSwipe.Normalize();


                    if (((CurrentSwipe.y > 0 && CurrentSwipe.x > -0.5f && CurrentSwipe.x < 0.5f) || Input.GetKeyDown(KeyCode.W)) && CurrentSquare.y < (WorldHeight.y - 1))
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        SetTarget(new Vector2(0, 1));
                    }
                    if ((((CurrentSwipe.x < 0 && CurrentSwipe.y > -0.5f && CurrentSwipe.y < 0.5f)) || Input.GetKeyDown(KeyCode.A)) && CurrentSquare.x > 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                        SetTarget(new Vector2(-1, 0));
                    }
                    if ((((CurrentSwipe.y < 0 && CurrentSwipe.x > -0.5f && CurrentSwipe.x < 0.5f)) || Input.GetKeyDown(KeyCode.S)) && CurrentSquare.y > 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                        SetTarget(new Vector2(0, -1));
                    }

                    if ((((CurrentSwipe.x > 0 && CurrentSwipe.y > -0.5f && CurrentSwipe.y < 0.5f)) || Input.GetKeyDown(KeyCode.D)) && CurrentSquare.x < (WorldHeight.x - 1))
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                        SetTarget(new Vector2(1, 0));
                    }
                    SecondPress = Vector2.zero;
                }
                else
                {
                    Firing();
                }
            }


        }
    }

    void PCTouch()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Firing();
        }
       
            if ((Input.GetKeyDown(KeyCode.W)) && CurrentSquare.y < (WorldHeight.y - 1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                SetTarget(new Vector2(0, 1));
            }
            if ((Input.GetKeyDown(KeyCode.A)) && CurrentSquare.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                SetTarget(new Vector2(-1, 0));
            }
            if (((Input.GetKeyDown(KeyCode.S)) && CurrentSquare.y > 0))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                SetTarget(new Vector2(0, -1));
            }

            if ((Input.GetKeyDown(KeyCode.D)) && CurrentSquare.x < (WorldHeight.x - 1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                SetTarget(new Vector2(1, 0));
            }

    }

    public AudioClip JumpSound;
    AudioSource PlayerAudio;
    void SetTarget(Vector2 Movement)
    {

        var PossibleSquare = new Vector2(CurrentSquare.x + Movement.x, CurrentSquare.y + Movement.y);
        if(GridSystem[(int)PossibleSquare.x, (int)PossibleSquare.y]!=null)
        {
            if (!GridSystem[(int)PossibleSquare.x, (int)PossibleSquare.y].GetComponent<GridSquare>().ObjectAbove() && GridSystem[(int)PossibleSquare.x, (int)PossibleSquare.y] != null)
            {
                GridSystem[(int)CurrentSquare.x, (int)CurrentSquare.y].GetComponent<GridSquare>().SetAbove(false);
                CurrentSquare = PossibleSquare;
                GridSystem[(int)CurrentSquare.x, (int)CurrentSquare.y].GetComponent<GridSquare>().SetAbove(true);
                Target = new Vector3(GridSystem[(int)CurrentSquare.x, (int)CurrentSquare.y].transform.position.x, transform.position.y, GridSystem[(int)CurrentSquare.x, (int)CurrentSquare.y].transform.position.z);
                startTime = Time.time;
                journeyLength = Vector3.Distance(transform.position, Target);
                if (reverb.enabled == true)
                {
                    reverb.enabled = false;
                }
                PlayerAudio.PlayOneShot(JumpSound, 0.5f);
                
            }
            ActionTaken = true;
        }
       
      

        
    }

    void PositionMovement()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, Target, fracJourney);
        if (Vector3.Distance(transform.position, Target)<0.001)
        {
            Target = Vector3.zero;
            ActionFinished();
            
        }
    }

    public GameObject Bullet;
    GameObject GunPos;

   public void Firing()
    {
          if(reverb.enabled==false)
          {
            reverb.enabled = true;
          }
          PlayerAudio.PlayOneShot(GunShot, 0.5f);
          var temp = Instantiate(Bullet, transform.position + (transform.forward * 0.8f), Quaternion.identity);
          Vector3 endPos = transform.position + (transform.forward * 3);
          ActionTaken = true;
          StartCoroutine(MovePos(temp, endPos, 0.5f));
          temp.transform.GetChild(0).transform.GetComponent<BulletCollision>().SetParentPos(transform.position);
          Destroy(temp, 7.0f);
 
    }

    IEnumerator MovePos(GameObject bullet, Vector3 endPos, float movement)
    {

        var t = 0f;
        var currentPos = bullet.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / movement;
            bullet.transform.position = Vector3.Lerp(currentPos, endPos, t);
            yield return null;
        }
        bullet.GetComponent<ParticleSystem>().Stop();
        if(bullet.transform.childCount>0)
        {
            Destroy(bullet.transform.GetChild(0).gameObject);
        }
        Destroy(bullet, 1f);
        Invoke("ActionFinished", 0.6f);

    }

    void ActionFinished()
    {
        ActionCompleted = true;
    }

  

	void Update ()
    {
   
            Movement();
      
	}
}
