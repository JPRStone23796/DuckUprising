using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float CamTimer, SetCamTimer,speed;
    bool bShaking;
    Vector3 originalPos;
    AudioSource AS;
    public AudioClip HitSound;

    void Awake()
    {
        originalPos = transform.position;
        SetCamTimer = .8f ;
        speed = 10f;
        bShaking = false;
        AS = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        StartCameraShake();
    }
    
	 void StartCameraShake()
    {
        if(CamTimer>0 && bShaking)
        {
            transform.position = originalPos;
            float seed = Time.time * speed;
            Vector2 Shake;
            Shake.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f) - 0.5f);
            Shake.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)-0.5f);
            transform.position = new Vector3(transform.position.x + Shake.x, transform.position.y + Shake.y, transform.position.z);
            CamTimer -= Time.deltaTime;
            //Handheld.Vibrate();
           
        }
        else if(CamTimer<=0 && bShaking==true)
        {
            bShaking = false;
            transform.position = originalPos;
        }
       
    }

    public void ResetShake()
    {
        bShaking = true;
        CamTimer = SetCamTimer;
        AS.PlayOneShot(HitSound, 0.5f);
    }
}
