using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundDelete : MonoBehaviour
{
    public GameObject spawn;
    public GameObject bkgDelete;

    // Start is called before the first frame update
    void Start()
    {
        bkgDelete = GameObject.Find("Backus Deletus");
        spawn = GameObject.Find("BkgSpawn6");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bkgDelete.transform.position.y)
        {
            transform.position = new Vector2(0, spawn.transform.position.y);
        }
    }
}
