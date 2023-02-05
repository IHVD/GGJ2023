using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float scoreFloat;
    public int score;
    public int highscore;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        highscore= 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        //highscoreText = GetComponent<TextMeshProUGUI>();
        //UpdateScore();
        Load();
    }

    
    // Update is called once per frame
    private void FixedUpdate()
    {

        scoreFloat += Time.deltaTime;
        score = (int)scoreFloat * 7;
        UpdateScore();

        //Debug.Log("Score:" + score);

        DeadTrigger script = GameObject.FindWithTag("Player").GetComponent<DeadTrigger>();
        if (script != null)
        {

            if (script.isDead)
            {
                           
                if (script.isDead == true) 
                {
                    

                    if (score > highscore)
                    {
                        highscore = score;
                        Save();
                    }

                    
                    Debug.Log("Highscore: " + highscore);

                }
            }

        }

    }
    private void UpdateScore()
    {
        //Debug.Log($"{score}, {highscore}, {scoreText}");
        if(scoreText == null)
        {
            return;
        }
        scoreText.text = "Score: " + score + "<br>Highscore: " + highscore;
        
        //highscoreText.text = "Highscore: " + highscore;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("highscore", highscore);
        Debug.Log("Score Saved");
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("highscore");
    }
}
