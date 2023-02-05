using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("UNGRAPPLED");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInChildren<RootShooter>().CancelGrapple();
            
        }
        
    }
}
