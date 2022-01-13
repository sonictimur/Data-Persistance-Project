using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public Text bestScore;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        CalculateBestScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void CalculateBestScore()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount");
        List<int> scores = new List<int>();

        string bestPlayerName = "N/A";
        int bestPlayerScore = 0;

        if (playerCount != 1 && playerCount != 0) 
        {
            //for (int i = 1; i <= playerCount; i++) // Calculating best score..
            //{
            //    int playerScore = PlayerPrefs.GetInt(i.ToString());
            //    scores.Add(playerScore);
            //}

            //scores.Sort();
            //bestPlayerScore = scores[0];

            List<List<string>> listOfPlayers = new List<List<string>>();

            for (int i = 1; i <= playerCount; i++)
            {
                string playerName = PlayerPrefs.GetString(i.ToString());

                int playerScore = Convert.ToInt32(PlayerPrefs.GetString(i.ToString() + "score", "0"));

                List<string> nameScore = new List<string>();
                nameScore.Add(playerName);
                nameScore.Add(playerScore.ToString());

                listOfPlayers.Add(nameScore);
            }

            listOfPlayers = listOfPlayers.OrderBy(lst => lst[0][1]).ToList();

            foreach (List<string> player in listOfPlayers)
            {
                Debug.Log(player[1]);
            }

            bestPlayerName = listOfPlayers[0][0];
            bestPlayerScore = Convert.ToInt32(listOfPlayers[0][1]);
        }

        bestScore.text = $"Best Score : {bestPlayerScore} || By Player Name: {bestPlayerName}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int playerCount = PlayerPrefs.GetInt("PlayerCount");
                int playerID = playerCount + 1;

                Debug.Log("There are " + playerCount + " players, so the next one is us!");

                PlayerPrefs.SetString(playerID.ToString() + "score", m_Points.ToString());
                PlayerPrefs.Save();

                LoadHighScoreScene();
            }
        }
    }
    
    void LoadHighScoreScene()
    {
        SceneManager.LoadScene("HighScore");
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
