using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    private static EndGameManager instance;
    public static EndGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = GameObject.Find("/EndGame Manager");
                if (go == null)
                {
                    return null;
                }

                instance = go.GetComponent<EndGameManager>();
                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }
    [Header("Nav Buttons")]

    public Button HomeButton;
    public Button RestartButton;
    public Button ExitButton;

    [Header("UI")]
    public TMP_Text StatusText;
    public TMP_Text HPText;
    public TMP_Text totalFireRateText;
    public TMP_Text totalHitText;
    public TMP_Text totalKillText;
    public TMP_Text totalDamageText;
    public TMP_Text distanceText;
    public TMP_Text timeText;
     
    float HP = 100;
    int totalFireRate = 0;
    int totalHit = 0;
    int totalKill = 0;
    float totalDamage = 0;
    public List<float> distanceArray; 
    string timeString = "";
    bool isDied = false;

    int ArrayLen = 5;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        HP = PlayerPrefs.GetFloat("HP");
        totalFireRate = PlayerPrefs.GetInt("totalFireRate");
        totalHit = PlayerPrefs.GetInt("totalHit");
        totalKill = PlayerPrefs.GetInt("totalKill");
        totalDamage = PlayerPrefs.GetFloat("totalDamage");

        timeString = PlayerPrefs.GetString("timeString");
        isDied = PlayerPrefs.GetInt("isDied") == 1 ? true : false;

        ArrayLen = PlayerPrefs.GetInt("ArrayLen");
        Debug.Log("ArrayLen: " + ArrayLen);
        for (int i = 0; i < ArrayLen; i++)
        {
            distanceArray.Add(PlayerPrefs.GetFloat("distanceArray" + i));
        }
        if (isDied)
        {
            StatusText.text = "You Died";
        }
        else
        {
            StatusText.text = "You Win";
        }
        HPText.text = HP.ToString();
        totalFireRateText.text = totalFireRate.ToString();
        totalHitText.text = totalHit.ToString();
        totalKillText.text = totalKill.ToString();
        totalDamageText.text = totalDamage.ToString();
        distanceText.text = FormatDistanceList(distanceArray);
        timeText.text = timeString;

        HomeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("StartScene");
        });
        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
        ExitButton.onClick.AddListener(() =>
        {
            if (Application.isEditor)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Debug.Log("Game in Editor");
            }
            else
            {
                Application.Quit();
                Debug.Log("Game is exiting");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string FormatDistanceList(List<float> distanceArray)
    {
        string formattedString = "Kill  Distances:";

        for (int i = 0; i < distanceArray.Count; i++)
        {
            float distance = distanceArray[i];
            string formattedDistance = distance.ToString("F2"); // Ýki ondalýk basamaða kadar formatlama

            formattedString += "\n" + formattedDistance; // Yeni satýra geçerek ekle

        }

        return formattedString;
    }
}
