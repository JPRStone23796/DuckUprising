using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    CameraShake shake;
    Vector3 ParentPos;
    void Awake()
    {
        shake = GameObject.Find("CameraParent").GetComponent<CameraShake>();

    }

    public void SetParentPos(Vector3 pos)
    {
        ParentPos = pos;
    }

    


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Destroy(this.gameObject);
            shake.ResetShake();
        }
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            shake.ResetShake();
            Destroy(other.gameObject);
        }
        else if (other.tag != "Enemy" )
        {
            Destroy(this.gameObject);
            shake.ResetShake();
        }
        else if (other.tag=="Enemy")
        {
            IEnemy enemy = (IEnemy)other.GetComponent(typeof(IEnemy));
            enemy.RemoveEnemy();
            Vector3 dir = other.transform.position - ParentPos;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * 30;
            Destroy(other.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 5.0f);
            Destroy(this.gameObject);
            shake.ResetShake();
        }

        


    }



}

