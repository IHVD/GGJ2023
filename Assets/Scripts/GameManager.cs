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
    public float scoreFloat;
    public int score;
    public int highscore;
    public bool isDead;
    
    

    private GameObject[] backgroundInstance;
    private GameObject[] foregroundInstance;

    void Start()
    {
        Load();
        isDead = false;
        scoreFloat = 0.0f;
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
    public void Save()
    {
        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("highscore");
    }

    private void FixedUpdate()
    {

        scoreFloat += Time.deltaTime;
        int score = (int)scoreFloat * 7;
        Debug.Log("Score:" + score);

        if (isDead == true) //Use this to end game
        {
            Save();
        
            if (score > highscore)
            {
                highscore = score;
            }
            Debug.Log("GAME OVER");
            Debug.Log("Highscore: " + highscore);

        }

        

    }

   
}
