using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public float cameraSpeed = 0.1f;

    void Start()
    {
        transform.position = player.position + cameraOffset;
    }

    void FixedUpdate()
    {
        Vector3 finalPosition = player.position + cameraOffset;
        float yLerp = Mathf.Lerp(transform.position.y, finalPosition.y, cameraSpeed);
        float zLerp = Mathf.Lerp(transform.position.z, finalPosition.z, cameraSpeed);
        transform.position = new Vector3(0, yLerp, zLerp);
    }

}
