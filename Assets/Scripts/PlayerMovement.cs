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
}
