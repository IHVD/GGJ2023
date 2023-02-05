using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    public GameObject endPanel;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        isDead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead == true)
        {
            Time.timeScale = 0f;
            endPanel.SetActive(true);
            Debug.Log("GAME OVER");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mole"))
        {
            Debug.Log($"{other.gameObject.tag}, {other.gameObject.name}, mine is {gameObject.tag}");
            isDead = true;
        }
    }
}
