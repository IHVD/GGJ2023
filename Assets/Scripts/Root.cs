using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

	public LineRenderer root;
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
	}
}
