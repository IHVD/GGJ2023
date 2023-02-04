using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    /*public LineRenderer root;
	public Transform target;
	public float hookLength = 2f;

	void Start()
	{
		Vector3 diff = target.position - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90f);

		diff *= hookLength;
		root.SetPosition(0, transform.position + new Vector3(diff.x, diff.y, 1f));
	}

	void LateUpdate()
	{
		if (target == null) return;
		Vector3 pos = target.position + (Vector3)target.GetComponent<DistanceJoint2D>().anchor;
		pos.z = 1f;
		root.SetPosition(1, pos);

		float distance = Vector2.Distance(transform.position, target.position);
		root.material.mainTextureScale = new Vector2(distance / 10f, .1f);
	}*/

    [Header("General Refernces:")]
    public RootShooter rootShootGun;
    public LineRenderer m_lineRenderer;
    public GameObject rootTip;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool straightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        straightLine = false;

        LinePointsToFirePoint();

        m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            m_lineRenderer.SetPosition(i, rootShootGun.firePoint.localPosition);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!straightLine)
        {
            if (m_lineRenderer.GetPosition(precision - 1).x == rootShootGun.grapplePoint.x)
            {
                straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if (!isGrappling)
            {
                rootShootGun.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(rootShootGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(rootShootGun.firePoint.position, rootShootGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(rootShootGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
            AddTippy(currentPosition);
            //Debug.Log("attached");
        }

        if (waveSize <= 0)
        {
			if (rootShootGun.hitObject.CompareTag("Rock")){
                rootShootGun.hitObject.GetComponent<RockObstacle>().isGrab = true;
                rootShootGun.CancelGrapple();
                Debug.Log("attached to rock");
			}
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, rootShootGun.firePoint.position);
        m_lineRenderer.SetPosition(1, rootShootGun.grapplePoint);
    }

    void AddTippy(Vector3 position)
	{
        rootTip.transform.position = position;
	}
}
