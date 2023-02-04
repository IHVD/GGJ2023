using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float scoreFloat;
    public int score;
    public int highscore;

    // Start is called before the first frame update
    void Start()
    {
        highscore= 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        scoreFloat += Time.deltaTime;
        int score = (int)scoreFloat * 7;
        //Debug.Log("Score:" + score);

        DeadTrigger script = gameObject.GetComponent<DeadTrigger>();
        if (script != null)
        {

            if (script.isDead)
            {


                

                if (script.isDead == true) 
                {
                    Save();

                    if (score > highscore)
                    {
                        highscore = score;
                    }
                    
                    Debug.Log("Highscore: " + highscore);

                }
            }

        }

    }

    public void Save()
    {
        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("highscore");
    }
}
