using System.Collections;
using System.Collections.Generic;
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
    public float HP = 50;
    public int totalFire=0;
    public int totalHit=0;
    public int totalKill=0;
    public int totalDamage=0;

    void MakeHp()
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc_OnClick();
        }
    }

    public void Esc_OnClick()
    {
        if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
        }
        else
        {
            PauseMenu.SetActive(true);
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
}
