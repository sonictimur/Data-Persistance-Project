using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject textObjPrefab;
    public List<GameObject> scoreObjectList;

    public GameObject scoreObjspawnPos;

    string playerName;
    int playerScore;

    int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        playerCount = PlayerPrefs.GetInt("PlayerCount");

        for (int i = 1; i <= playerCount; i++) // For each player count...
        {
            string playerID = i.ToString();

            playerName = PlayerPrefs.GetString(playerID);

            Debug.Log(PlayerPrefs.GetString((Convert.ToInt32(playerID) + 1).ToString() + "score", "0"));
            playerScore = Convert.ToInt32(PlayerPrefs.GetString((Convert.ToInt32(playerID) + 1).ToString() + "score", "0"));

            Debug.Log("Here is one score: " + playerScore);

            GameObject textObj = Instantiate(textObjPrefab, scoreObjspawnPos.transform.position, textObjPrefab.transform.rotation);
            textObj.transform.SetParent(scoreObjspawnPos.transform);

            scoreObjectList.Add(textObj);

            textObj.transform.Translate(0, -60 * scoreObjectList.Count, 0);
            textObj.GetComponent<TMP_Text>().text = "Player Name: " + playerName + " || Score: " + playerScore;
        }

        PlayerPrefs.SetInt("PlayerCount", scoreObjectList.Count);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
