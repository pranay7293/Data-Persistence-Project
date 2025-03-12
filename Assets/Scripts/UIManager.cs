using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   
    public TMP_InputField inputField;
    public TextMeshProUGUI topScoresText; // Assign in Inspector


    private void Start()
    {
        inputField.onValueChanged.AddListener(SetName);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScores(); // Load scores from JSON
            DisplayTopScores();
        }

    }

    private void SetName(string name)
    {
        GameManager.Instance.playerName = name;
    }

    public void LoadTheGame()
    {
        SceneManager.LoadScene(1);

    }

    void DisplayTopScores()
    {
        var topScores = GameManager.Instance.scoreboard.topScores;

        if (topScores == null || topScores.Count == 0)
        {
            topScoresText.text = "No high scores yet!";
            return;
        }

        string scoreDisplay = "🏆 **Top 5 Scores** 🏆\n";
        for (int i = 0; i < topScores.Count; i++)
        {
            scoreDisplay += $"{i + 1}. {topScores[i].playerName} - {topScores[i].score}\n";
        }

        topScoresText.text = scoreDisplay; // Update UI text
    }
}
