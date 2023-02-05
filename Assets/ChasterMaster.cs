using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChasterMaster : MonoBehaviour
{
    public float chaseOffset;
    public float distanceToPlayer;
    public float howLongUntilPlayer;
    public Transform player;

    public Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(31560, 50);
    }

    // Update is called once per frame
    void Update()
    {
        //r2d.DOMove(player.position, howLongUntilPlayer -= Time.deltaTime);
        //chaseOffset = Vector2.Lerp(transform.position, player.position, howLongUntilPlayer -= Time.deltaTime);

    }

	private void FixedUpdate()
	{
        transform.position = new Vector3(transform.position.x, player.position.y - chaseOffset, transform.position.z);

	}
}
