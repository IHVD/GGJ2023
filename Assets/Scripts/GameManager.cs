using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public GameObject foregroundPrefab;
    public Transform[] wallSpawnPoints;
    public Transform[] bkgSpawnPoints;
    public float speed = 1.0f;
    public float bkgSpeed = 1.2f;
    public float speedIncrement = 0.1f;
    public Rigidbody2D playerRb;

    public bool isDead;

    private GameObject[] backgroundInstance;

    void Start()
    {
        Time.timeScale= 0f;
        playerRb.simulated = false;

        
        backgroundInstance = new GameObject [bkgSpawnPoints.Length];
        for (int i = 0; i <bkgSpawnPoints.Length; i++)
        {
            backgroundInstance[i] = Instantiate(backgroundPrefab, bkgSpawnPoints[i].position, Quaternion.identity);
        }
    }

    void Update()
    {
        for (int i = 0; i < bkgSpawnPoints.Length; i++)
        {
            backgroundInstance[i].transform.position -= new Vector3(0, bkgSpeed * Time.deltaTime, 0);

        }
        bkgSpeed += speedIncrement * Time.deltaTime;

        speed += speedIncrement * Time.deltaTime;
                      
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(2);
    } 
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
