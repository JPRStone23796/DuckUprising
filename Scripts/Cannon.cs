using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IEnemy
{
    EnemyClass CurrentEnemy;
    [SerializeField]
    GameObject cannonBall;
    Transform cannonPos;
    bool actionFinished;
    bool movementStarted;

    void Start()
    {
        cannonPos = transform.GetChild(0).transform;
        actionFinished = false;
        movementStarted = false;
    }


    public void ResetAction()
    {
        actionFinished = false;
        movementStarted = false;
    }


    public void SetID(EnemyID id)
    {
        CurrentEnemy.SetID(id);
    }

    public void SetLM(LevelManager LM)
    {
        CurrentEnemy.SetLevelManager(LM);
    }

    public void SetPos(Vector2 startPos)
    {
        CurrentEnemy = new EnemyClass();
        CurrentEnemy.UpdatePosition(startPos);
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


        CurrentRotation = pos + 1;
    }


    public void RemoveEnemy()
    {
        CurrentEnemy.RetreiveLevelManager().RemoveEnemy(CurrentEnemy.RetreiveID());
    }

    int CurrentRotation = 0;
    Quaternion TargetRot;
    [SerializeField]
    float RotationSpeed = 0.5f;

    public bool TakeAction()
    {
        if(movementStarted ==false)
        {
            switch (CurrentRotation)
            {
                case 0: TargetRot = Quaternion.Euler(new Vector3(0, 0, 0)); break;
                case 1: TargetRot = Quaternion.Euler(new Vector3(0, 90, 0)); break;
                case 2: TargetRot = Quaternion.Euler(new Vector3(0, 180, 0)); break;
                case 3: TargetRot = Quaternion.Euler(new Vector3(0, 270, 0)); break;
            }

            if (CurrentRotation < 3)
            {
                CurrentRotation++;
            }
            else
            {
                CurrentRotation = 0;
            }

            StartCoroutine(PrepareCannon());
        }
       
        return actionFinished;
    }

    IEnumerator PrepareCannon()
    {
        bool bAtTargetRotation = false;
        movementStarted = true;
        while(bAtTargetRotation==false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, RotationSpeed * Time.deltaTime);
            if((transform.rotation.eulerAngles-TargetRot.eulerAngles).magnitude<0.5f)
            {
                bAtTargetRotation = true;
            }
            yield return null;
        }

        GameObject cannon = Instantiate(cannonBall, cannonPos.position, transform.rotation);
        Rigidbody rb = cannon.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 25;
        Destroy(cannon, 3.0f);
        actionFinished = true;
        yield return null;
    }

    
}
