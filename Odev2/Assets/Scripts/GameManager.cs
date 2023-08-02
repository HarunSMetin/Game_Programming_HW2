using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = GameObject.Find("/Game Manager");
                if (go == null)
                {
                    return null;
                }

                instance = go.GetComponent<GameManager>();
                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    [Header("Player UI")]
    public GameObject PlayerPoint;
    public TMP_Text PlayerNameTMP;
    public TMP_Text PlayerHP;
    public Slider PlayerHPSlider; 
    public TMP_Text WeaponTemp;
    public TMP_Text LeftEnemy;
    public TMP_Text WeaponBattery;
    public TMP_Text timeText;
    public GameObject LRFVision;

    [Header("Prefabs")]
    public GameObject[] Characters;
    public GameObject[] Weapons;
    public GameObject[] Eqs;
     

    public string playerName = "Player";
    public GameObject currentChar;
    public GameObject currentWeapon;
    public GameObject currentEq;

    public GameData.difficulty currentDiff;
    public GameData.character currentCharData;
    public GameData.weapon currentWeaponData;
    public GameData.eq currentEqData;


    [Header("PauseMenu")]
    public GameObject PauseMenu;

    [Header("Game Values")]
    public float HP = 100;
    public int totalFireRate=0;
    public int totalHit=0;
    public int totalKill=0;
    public float totalDamage=0;
    public List<float> distanceArray;
    public float TotalTime=0;
    public string timeString="";
    public bool isDied = false;
    public void MakeHp()
    {
        PlayerHP.text = "%"+HP.ToString();
        PlayerHPSlider.value = HP;
    }

    void Awake()
    { 
        currentDiff = (GameData.difficulty)PlayerPrefs.GetInt("Diff");
        currentCharData = (GameData.character)PlayerPrefs.GetInt("Char");
        currentWeaponData = (GameData.weapon)PlayerPrefs.GetInt("Weapon"); 
        currentEqData = (GameData.eq)PlayerPrefs.GetInt("Eq");

        playerName = PlayerPrefs.GetString("UserName");
        currentChar = Characters[(int)currentCharData];
        currentWeapon = Weapons[(int)currentWeaponData];
        currentEq = Eqs[(int)currentEqData];

        PlayerNameTMP.text = playerName;
        MakeHp();

        Instantiate(currentChar, PlayerPoint.transform.position, PlayerPoint.transform.rotation, PlayerPoint.transform);

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        switch (currentDiff)
        {
            case GameData.difficulty.easy:
                EnemyManager.Instance.enemyCount = 5;
                break;
            case GameData.difficulty.medium:
                EnemyManager.Instance.enemyCount = 10;
                break;
            case GameData.difficulty.hard:
                EnemyManager.Instance.enemyCount = 15;
                break; 
            default:
                break;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDied)
            UpdateTimer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc_OnClick();
        }
    }
    private void UpdateTimer()
    {
        TotalTime += Time.deltaTime;

        // Zamaný formatlayarak UI'da gösterelim
        int minutes = Mathf.FloorToInt(TotalTime / 60f);
        int seconds = Mathf.FloorToInt(TotalTime % 60f);
        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Text elemanýna formatlanmýþ zamaný atayalým
        timeText.text = "Time: " + timeString;
    }
    public void Esc_OnClick()
    {
        if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            PauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void loadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
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

    }
    public void EndGame()
    {
        PlayerPrefs.SetFloat("HP", HP);
        PlayerPrefs.SetInt("totalFireRate", totalFireRate);
        PlayerPrefs.SetInt("totalHit", totalHit);
        PlayerPrefs.SetInt("totalKill", totalKill);
        PlayerPrefs.SetFloat("totalDamage", totalDamage);
        PlayerPrefs.SetString("timeString", timeString);
        PlayerPrefs.SetInt("isDied", isDied ? 1 : 0);

        PlayerPrefs.SetInt("ArrayLen", distanceArray.Count);
        for (int i = 0; i < distanceArray.Count; i++)
        {
            PlayerPrefs.SetFloat("distanceArray" + i.ToString(), distanceArray.ElementAt(i));
        } 

        SceneManager.LoadScene("FinishScene"); 
    }
}
