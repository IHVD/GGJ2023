using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugGenerator : MonoBehaviour
{
    public GameObject prefab;
    public float pointAX;
    public float pointBX;
    public float yMin = -1f;
    public float yMax = 1f;
    public float speed = 5f;

    public Transform player;

    private void Start()
    {
        Vector3 startingPoint = new Vector3(pointAX, Random.Range(yMin, yMax), 0);
        Vector3 targetPoint = startingPoint.x == pointAX ? new Vector3(pointBX, Random.Range(yMin, yMax), 0) : new Vector3(pointAX, Random.Range(yMin, yMax), 0);

        //Debug.Log(startingPoint+ " " + targetPoint);
        // Instantiate the prefab at the starting point
        GameObject instance = Instantiate(prefab, startingPoint, Quaternion.identity);

        // Start the movement coroutine
        StartCoroutine(MoveFromPointToPoint(instance, startingPoint, targetPoint));
    }

    private IEnumerator MoveFromPointToPoint(GameObject instance, Vector3 startingPoint, Vector3 targetPoint)
    {
        while (instance.transform.position != targetPoint)
        {
            instance.transform.position = Vector3.MoveTowards(instance.transform.position, targetPoint, Time.deltaTime * speed);
            yield return null;
        }
        Destroy(instance);

        SpawnBug();

	}

    public void SpawnBug()
    {
        
		Vector3 startingPoint = new Vector3(pointAX, Random.Range(yMin, yMax) + player.transform.position.y + 20, 0);
		Vector3 targetPoint = startingPoint.x == pointAX ? new Vector3(pointBX, Random.Range(yMin, yMax), 0) : new Vector3(pointAX, Random.Range(yMin, yMax), 0);

		//Debug.Log(startingPoint+ " " + targetPoint);
		// Instantiate the prefab at the starting point
		GameObject instance = Instantiate(prefab, startingPoint, Quaternion.identity);
		StartCoroutine(MoveFromPointToPoint(instance, startingPoint, targetPoint));
	}
}