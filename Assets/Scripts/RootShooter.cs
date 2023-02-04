using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class RootShooter : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public Root grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform rootShootHolder;
    public Transform rootShootPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    //public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    [SerializeField] public GameObject hitObject;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        grappleRope.enabled = false;
        grappleRope.rootTip.SetActive(false);
        //m_springJoint2D.enabled = false;

    }

    private void Update()
    {
        Vector3 actualMousePos = Input.mousePosition;
        actualMousePos.z = -20f;


        Touch touch = new Touch();
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || touch.phase == TouchPhase.Began && Input.touchCount > 0)
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0) || touch.phase == TouchPhase.Moved && Input.touchCount > 0)
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 screenpos = new Vector2(0,0);
                screenpos = m_camera.ScreenToWorldPoint(Input.touchCount > 0 ? touch.position : actualMousePos);

                RotateGun(screenpos, true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || touch.phase == TouchPhase.Ended && Input.touchCount == 0)
        {
            CancelGrapple();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.touchCount > 0 ? touch.position : actualMousePos);
            RotateGun(mousePos, true);
            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistance = firePoint.position - rootShootHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistance;
                    rootShootHolder.position = Vector2.Lerp(rootShootHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - rootShootPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            rootShootPivot.rotation = Quaternion.Lerp(rootShootPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            rootShootPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void CancelGrapple()
	{
        grappleRope.enabled = false;
        grappleRope.rootTip.SetActive(false);
        //m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
    }

    void SetGrapplePoint()
    {
        Vector3 actualMousePos = Input.mousePosition;
        actualMousePos.z = -20f;
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(actualMousePos) - rootShootPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)rootShootPivot.position;
                    grappleRope.enabled = true;
                    grappleRope.rootTip.SetActive(true);

                    hitObject = _hit.transform.gameObject;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
