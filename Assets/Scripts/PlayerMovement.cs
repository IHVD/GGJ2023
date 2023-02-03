using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput pInput;
    public Camera pCamera;
    public Rigidbody2D r2d;

    public GameObject rootPrefab;
    public DistanceJoint2D playerJoint;

    private GameObject currentRoot;

    // Start is called before the first frame update
    void Start()
    {
        if(pInput == null)
        {
            pInput = GetComponent<PlayerInput>();
        }

        if(pCamera == null)
        {
            pCamera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        //ProcessInput();
    }

    void ProcessInput()
    {

    }

    void OnTouch()
    {
        Vector3 clickPos = Mouse.current.position.ReadValue();
        clickPos.z = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(clickPos);


        //Debug.Log($"normie: {clickPos.normalized}, dir: {dir}");

        int layerMask = 1 << LayerMask.NameToLayer("Grabbable");
        Debug.DrawLine(r2d.transform.position, worldPos, Color.black, 2.5f);
        
        RaycastHit2D hit = Physics2D.Raycast(r2d.transform.position, worldPos, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            GameObject newRoot = Instantiate(rootPrefab, new Vector3(0f, 0f, -2f) + new Vector3(hit.point.x, hit.point.y), Quaternion.identity);
            newRoot.GetComponent<Root>().target = playerJoint.transform;
            playerJoint.autoConfigureDistance = true;
            playerJoint.connectedBody = newRoot.GetComponent<Rigidbody2D>();

            if(currentRoot == null)
            {
                currentRoot = newRoot;
            }
            else
            {
                Destroy(currentRoot);
                currentRoot = newRoot;
            }
        }
       
    }

    void OnTest()
    {
        Debug.Log("wtf");
    }
}
