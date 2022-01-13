using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static string playerName;
    public TMP_InputField input;

    public string inputedName;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void Validate()
    {
        if (input.text == "")
        {
            Debug.Log("Empty name not allowed!");
        }
    }

    public void SaveInput()
    {
        if (input.text != "")
        {
            inputedName = input.text;
            int playerID = PlayerPrefs.GetInt("PlayerCount", 0);

            PlayerPrefs.SetString((playerID + 1).ToString(), inputedName);
            PlayerPrefs.SetInt("PlayerCount", (PlayerPrefs.GetInt("PlayerCount", 0) + 1));
            PlayerPrefs.Save();

            SceneManager.LoadScene("main");

        } else if (input.text == "")
        {
            Debug.Log("Empty name not allowed! Please enter something before proceeding!");
        }
    }

    public void LoadHighScore()
    {
        SceneManager.LoadScene("HighScore");
    }
}
