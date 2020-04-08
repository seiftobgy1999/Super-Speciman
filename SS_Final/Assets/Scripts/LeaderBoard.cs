using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    public Text Names;
    public Text Scores;
    public Button goHomeButton;
    public List<string> scoreVals = new List<string>();
   
    // Start is called before the first frame update
    void Start()
    {
    goHomeButton.onClick.AddListener(() => goHome());

        string leaderboardPath = "./Assets/Data/Leaderboard.txt";
        string[] lines = System.IO.File.ReadAllLines(leaderboardPath);

        Dictionary<string,string> scores = new Dictionary<string,string>();
        for (int i = 0; i < lines.Length; i++) {
            scores.Add(lines[i].Split(',')[0],lines[i].Split(',')[1]);
            scoreVals.Add(lines[i].Split(',')[1]);
        }

        scoreVals.Sort((a, b) => b.CompareTo(a));
        string finalNames = "NAMES" + Environment.NewLine;
        string finalScores = "TOP SCORES" + Environment.NewLine;
        for (int i = 0; i < scoreVals.Count; i++) {
            finalNames += scores.FirstOrDefault(x => x.Value == scoreVals[i]).Key + Environment.NewLine;
            finalScores += scoreVals[i] + Environment.NewLine;
            if (i == 2) {
                break;
            }
        }
        Scores.text = finalNames;
        Names.text = finalScores;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void goHome() {
        SceneManager.LoadScene("Home Scene");
    }
}
