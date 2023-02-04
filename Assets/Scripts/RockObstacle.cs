using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isGrab;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrab == true)
        {
            rb.gravityScale = 1f;
        }
        
    }
}
