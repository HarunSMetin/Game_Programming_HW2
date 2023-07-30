using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Prefabs")]
    public GameObject[] Characters;
    public GameObject[] Weapons;
    public GameObject[] Eqs;

    public string playerName = "Player";
    public GameObject currentChar;
    public GameObject currentWeapon;
    public GameObject currentEq;

    public enum difficulty { easy, medium, hard };
    difficulty currentDiff = difficulty.easy;

    [Header("PauseMenu")]
    public GameObject PauseMenu;

    void Start()
    {
        playerName = PlayerPrefs.GetString("UserName");
        currentChar = Characters[PlayerPrefs.GetInt("Char")];
        currentWeapon = Weapons[PlayerPrefs.GetInt("Weapon")];
        currentEq = Eqs[PlayerPrefs.GetInt("Eq")];
        currentDiff = (difficulty)PlayerPrefs.GetInt("Diff");

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
