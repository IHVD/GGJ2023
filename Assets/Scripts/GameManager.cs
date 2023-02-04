using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public Transform[] spawnPoints;
    public float speed = 1.0f;
    public float speedIncrement = 0.1f;


    private GameObject[] backgroundInstance;

    void Start()
    {
        backgroundInstance = new GameObject [spawnPoints.Length];
        for (int i = 0; i <spawnPoints.Length; i++)
        {
            backgroundInstance[i] = Instantiate(backgroundPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            backgroundInstance[i].transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        }
            speed += speedIncrement * Time.deltaTime;


    }

   
}
