using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDelete : MonoBehaviour
{
    public GameObject spawn;
    public GameObject wallDelete;

    // Start is called before the first frame update
    void Start()
    {
        wallDelete = GameObject.Find("Wallus Deletus");
        spawn = GameObject.Find("WallSpawn4");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= wallDelete.transform.position.y)
        {
            transform.position = new Vector2(-3.5f, spawn.transform.position.y);
        }
    }

   
}
