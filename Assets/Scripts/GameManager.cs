using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public GameObject foregroundPrefab;
    public Transform[] wallSpawnPoints;
    public Transform[] bkgSpawnPoints;
    public float speed = 1.0f;
    public float bkgSpeed = 1.2f;
    public float speedIncrement = 0.1f;
    
   
    public bool isDead;
    
    

    private GameObject[] backgroundInstance;
    private GameObject[] foregroundInstance;

    void Start()
    {
        
        backgroundInstance = new GameObject [bkgSpawnPoints.Length];
        for (int i = 0; i <bkgSpawnPoints.Length; i++)
        {
            backgroundInstance[i] = Instantiate(backgroundPrefab, bkgSpawnPoints[i].position, Quaternion.identity);
        }

        foregroundInstance = new GameObject[wallSpawnPoints.Length];
        for (int i = 0; i < wallSpawnPoints.Length; i++)
        {
            foregroundInstance[i] = Instantiate(foregroundPrefab, wallSpawnPoints[i].position , Quaternion.identity);
        }
    }

    void Update()
    {
        for (int i = 0; i < bkgSpawnPoints.Length; i++)
        {
            backgroundInstance[i].transform.position -= new Vector3(0, bkgSpeed * Time.deltaTime, 0);

        }
        bkgSpeed += speedIncrement * Time.deltaTime;

        for (int i = 0; i < wallSpawnPoints.Length; i++)
        {
            foregroundInstance[i].transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        }

        speed += speedIncrement * Time.deltaTime;

        
        

    }
   

   

    


}
