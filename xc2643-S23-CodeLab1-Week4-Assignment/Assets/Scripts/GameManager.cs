using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gameLength = 10;
    public float timer = 0;

    public TextMeshPro displayText;

    bool inGame = true;

    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public List<int> highScores = new List<int>();

    string FILE_PATH;
    const string FILE_DIR  = "/Data/";
    const string FILE_NAME = "highScores.txt";
    
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timer = 0;

        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
    }
    
    void Update()
    {

        if (inGame)
        {
            timer += Time.deltaTime;
            displayText.text = "Timer: " + (gameLength - (int)timer) + "\n" +
                               "Score: " + score;
        }

        if (timer >= gameLength && inGame) 
        {
            inGame = false;
            Debug.Log("Game OVA!");
            SceneManager.LoadScene("EndScreen");
            UpdateHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (highScores.Count == 0) 
        {
            if (File.Exists(FILE_PATH))
            {
                string fileContents = File.ReadAllText(FILE_PATH);
                string[] fileSplit = fileContents.Split('\n');
                
                for (int i = 1; i < fileSplit.Length - 1; i++)
                {
                    highScores.Add(Int32.Parse(fileSplit[i]));
                }
            }
            else
            {
                highScores.Add(0);
            }
        }

        //insert our score into the high scores list, if it's large enough
        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i] < Score)
            {
                highScores.Insert(i, Score);
                break;
            }
        }

        if (highScores.Count > 5)
        {

            highScores.RemoveRange(5, highScores.Count - 5);
        }
        
        string highScoreStr = "High Scores:\n";

        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreStr += highScores[i] + "\n";
        }
        
        displayText.text = highScoreStr;
        
        File.WriteAllText(FILE_PATH, highScoreStr);
    }
}
