using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput pInput;

    // Start is called before the first frame update
    void Start()
    {
        if(pInput == null)
        {
            gameObject.GetComponent<PlayerInput>();
        }
    }

    private void FixedUpdate()
    {
        ProcessInput();
    }

    void ProcessInput()
    {

    }

    void OnTouch()
    {

    }

    void OnTest()
    {
        Debug.Log("wtf");
    }
}
