using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class Scoreboard
    {
        public List<ScoreEntry> topScores = new List<ScoreEntry>();
    }

    public static GameManager Instance;
    public string playerName;
    public Scoreboard scoreboard = new Scoreboard();

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/highscores.json";
        LoadScores();
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            scoreboard = JsonUtility.FromJson<Scoreboard>(json);
        }
    }

    public void AddNewScore(string playerName, int score)
    {
        // Add new score
        scoreboard.topScores.Add(new ScoreEntry { playerName = playerName, score = score });

        // Sort the scores in descending order and keep only the top 5
        scoreboard.topScores.Sort((a, b) => b.score.CompareTo(a.score));
        if (scoreboard.topScores.Count > 5)
        {
            scoreboard.topScores.RemoveAt(5);
        }

        SaveScores();
    }
}
