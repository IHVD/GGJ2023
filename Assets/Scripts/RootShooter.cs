using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityEngine.InputSystem;

public class RootShooter : MonoBehaviour
{
    [Header("Gameplay Variables:")]
    public int rootAmount;
    public int currentRoots;

    public Rigidbody2D r2d;

    public List<Vector2> grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    public LineRenderer lr;
    public LayerMask grappleMask;
    public float moveSpeed = 2;
    public float grappleLength = 5;

    private void Start()
    {
        //m_springJoint2D[0].enabled = false;
        if(r2d == null)
		{
            r2d = GetComponent<Rigidbody2D>();
		}
    }

    private void Update()
    {
        /*Vector3 actualMousePos = Input.mousePosition;
        actualMousePos.z = -20f;

        Touch touch = new Touch();
        if (Input.touchSupported)
            touch = Input.GetTouch(0);

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
            for(int p = 0; p < grapplePoint.Length; p++)
			{
                grapplePoint[p] = m_rigidbody.transform.position;
                grappleRope[p].enabled = false;
                grappleRope[p].isGrappling = false;
            }
		}

        if (Input.GetKeyDown(KeyCode.Mouse0) || touch.phase == TouchPhase.Began && Input.touchCount > 0)
        {
            SetGrapplePoint(currentRoots);
        }
        else if (Input.GetKey(KeyCode.Mouse0) || touch.phase == TouchPhase.Moved && Input.touchCount > 0)
        {
            if (grappleRope[currentRoots].enabled)
            {
                RotateGun(grapplePoint[currentRoots], false);
            }
            else
            {
                Vector2 screenpos = new Vector2(0,0);
                screenpos = m_camera.ScreenToWorldPoint(Input.touchCount > 0 ? touch.position : actualMousePos);

                RotateGun(screenpos, true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || touch.phase == TouchPhase.Ended && Input.touchCount > 0)
        {
            //grappleRope[currentRoots].enabled = false;
            //m_springJoint2D[0].enabled = false;
            m_rigidbody.gravityScale = 1;

            if (currentRoots < rootAmount - 1) //2 cuz if 3 we have 0 1 2 3 which is 4. (-1 if set to 3 in inspector)
                currentRoots++;
            else
                currentRoots = 0;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.touchCount > 0 ? touch.position : actualMousePos);
            RotateGun(mousePos, true);
            if (launchToPoint && (grappleRope[0].isGrappling || grappleRope[1].isGrappling || grappleRope[2].isGrappling))
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistance = firePoint.position - rootShootHolder.localPosition;
                    //singular Vector2 targetPos = grapplePoint[currentRoots] - firePointDistance; //MIDDLE OF 3 POINTS
                    Vector2 targetPos = new Vector2();
                    foreach(Vector2 pos in grapplePoint)
					{
                        if (pos == Vector2.zero) {
                            targetPos += (Vector2)m_rigidbody.transform.position;
                            
                            break;
						}
						else
						{
                            targetPos += pos;
						}
					}

                    rootShootHolder.position = Vector2.Lerp(rootShootHolder.position, targetPos / rootAmount, Time.deltaTime * launchSpeed);
                }
            }
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleMask);
            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                grapplePoint.Add(hitPoint);

                if (grapplePoint.Count > rootAmount)
                {
                    grapplePoint.RemoveAt(0);
                }
            }
        }

        if (grapplePoint.Count > 0)
        {
            Vector2 moveTo = centriod(grapplePoint.ToArray());
            r2d.DOMove(moveTo, Time.deltaTime * moveSpeed);

            lr.positionCount = 0;
            lr.positionCount = grapplePoint.Count * 2;
            for (int n = 0, j = 0; n < grapplePoint.Count * 2; n += 2, j++)
            {
                lr.SetPosition(n, transform.position);
                lr.SetPosition(n + 1, grapplePoint[j]);
            }
        }

        if (Input.GetKey(KeyCode.Space) && grapplePoint.Count > 0)
        {
            Detatch();
        }
    }

    public void Detatch()
    {
        lr.positionCount = 0;
        grapplePoint.Clear();
    }

    Vector2 centriod(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }

    //void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    //{
    //    Vector3 distanceVector = lookPoint - rootShootPivot.position;
    //
    //    float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
    //    if (rotateOverTime && allowRotationOverTime)
    //    {
    //        rootShootPivot.rotation = Quaternion.Lerp(rootShootPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    //    }
    //    else
    //    {
    //        rootShootPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    }
    //}

    /*void SetGrapplePoint(int rootToShoot)
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
                    grapplePoint[rootToShoot] = _hit.point;
                    grappleDistanceVector = grapplePoint[rootToShoot] - (Vector2)rootShootPivot.position;
                    grappleRope[rootToShoot].enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D[0].autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            //m_springJoint2D[0].distance = targetDistance;
            //m_springJoint2D[0].frequency = targetFrequency;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                //m_springJoint2D[0].autoConfigureDistance = true;
                //m_springJoint2D[0].autoConfigureDistance = true;
                //m_springJoint2D[0].frequency = 0;
            }

            //m_springJoint2D[0].connectedAnchor = grapplePoint[currentRoots];
            Debug.Log("setting joint");
            //m_springJoint2D[0].enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D[0].connectedAnchor = grapplePoint[currentRoots];
                    Debug.Log($"setting joint2 {grapplePoint[currentRoots]}");

                    Vector2 distanceVector = firePoint.position - rootShootHolder.position;

                    m_springJoint2D[0].distance = distanceVector.magnitude;
                    //m_springJoint2D[0].frequency = launchSpeed;
                    m_springJoint2D[0].enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    //m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }*/

    //private void OnDrawGizmosSelected()
    //{
    //    if (firePoint != null && hasMaxDistance)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(firePoint.position, maxDistance);
    //    }
    //}
}
