using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserSettingsManager : MonoBehaviour
{
    private static UserSettingsManager instance;
    public static UserSettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = GameObject.Find("/User Settings Manager");
                if (go == null)
                {
                    return null;
                }

                instance = go.GetComponent<UserSettingsManager>();
                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    [Header("User Preferences")]
    public string UserName = "Player";
    public GameData.character currentChar = GameData.character.char1;
    public GameData.weapon currentWeapon = GameData.weapon.weapon1;
    public GameData.eq currentEq = GameData.eq.eq1;
    public GameData.difficulty currentDiff = GameData.difficulty.easy;



    [Header("Sprites")]
    public Sprite CharSelected;
    public Sprite CharUnselected;
    Sprite WeaponSelected;
    Sprite WeaponUnselected;
    public Sprite EqSelected;
    public Sprite EqUnselected;

    [Header("UI")]
    public TMP_InputField inputField;
    public Button char1;
    public Button char2;
    public Button char3;
    public Button weapon1;
    public Button weapon2;
    public Button weapon3;
    public Button eq1;
    public Button eq2;
    public Button eq3;
    public Slider Difficulty;

    public Image SelectedChar1;
    public Image SelectedChar2;
    public Image SelectedChar3;

    public Image SelectedWeapon1;
    public Image SelectedWeapon2;
    public Image SelectedWeapon3;

    private void Start()
    {
        WeaponSelected = CharSelected;
        WeaponUnselected = CharUnselected;
        LoadUserSettings();
    }
    void ChangeSpriteOfButton(Button button, Sprite sprite)
    {
        button.image.sprite = sprite;

    }
    public void CharButtonOnClick(Button charButton)
    {
        switch (charButton.name)
        {
            case "Char1":
                currentChar = GameData.character.char1;
                ChangeSpriteOfButton(char1, CharSelected);
                ChangeSpriteOfButton(char2, CharUnselected);
                ChangeSpriteOfButton(char3, CharUnselected);
                SelectedChar1.gameObject.SetActive(true);
                SelectedChar2.gameObject.SetActive(false);
                SelectedChar3.gameObject.SetActive(false);

                break;
            case "Char2":
                currentChar = GameData.character.char2;
                ChangeSpriteOfButton(char1, CharUnselected);
                ChangeSpriteOfButton(char2, CharSelected);
                ChangeSpriteOfButton(char3, CharUnselected);
                SelectedChar1.gameObject.SetActive(false);
                SelectedChar2.gameObject.SetActive(true);
                SelectedChar3.gameObject.SetActive(false);
                break;
            case "Char3":
                currentChar = GameData.character.char3;
                ChangeSpriteOfButton(char1, CharUnselected);
                ChangeSpriteOfButton(char2, CharUnselected);
                ChangeSpriteOfButton(char3, CharSelected);
                SelectedChar1.gameObject.SetActive(false);
                SelectedChar2.gameObject.SetActive(false);
                SelectedChar3.gameObject.SetActive(true);
                break;
        }
    }
    public void WeaponButtonOnClick(Button weaponButton)
    {
        switch (weaponButton.name)
        {
            case "Weapon 1":
                currentWeapon = GameData.weapon.weapon1;
                ChangeSpriteOfButton(weapon1, WeaponSelected);
                ChangeSpriteOfButton(weapon2, WeaponUnselected);
                ChangeSpriteOfButton(weapon3, WeaponUnselected);
                SelectedWeapon1.gameObject.SetActive(true);
                SelectedWeapon2.gameObject.SetActive(false);
                SelectedWeapon3.gameObject.SetActive(false);
                break;
            case "Weapon 2":
                currentWeapon = GameData.weapon.weapon2;
                ChangeSpriteOfButton(weapon1, WeaponUnselected);
                ChangeSpriteOfButton(weapon2, WeaponSelected);
                ChangeSpriteOfButton(weapon3, WeaponUnselected);
                SelectedWeapon1.gameObject.SetActive(false);
                SelectedWeapon2.gameObject.SetActive(true);
                SelectedWeapon3.gameObject.SetActive(false);
                break;
            case "Weapon 3":
                currentWeapon = GameData.weapon.weapon3;
                ChangeSpriteOfButton(weapon1, WeaponUnselected);
                ChangeSpriteOfButton(weapon2, WeaponUnselected);
                ChangeSpriteOfButton(weapon3, WeaponSelected);
                SelectedWeapon1.gameObject.SetActive(false);
                SelectedWeapon2.gameObject.SetActive(false);
                SelectedWeapon3.gameObject.SetActive(true);
                break;
        }
    }
    public void EqButtonOnClick(Button eqButton)
    {
        switch (eqButton.name)
        {
            case "Eq1":
                currentEq = GameData.eq.eq1;
                ChangeSpriteOfButton(eq1, EqSelected);
                ChangeSpriteOfButton(eq2, EqUnselected);
                ChangeSpriteOfButton(eq3, EqUnselected);
                break;
            case "Eq2":
                currentEq = GameData.eq.eq2;
                ChangeSpriteOfButton(eq1, EqUnselected);
                ChangeSpriteOfButton(eq2, EqSelected);
                ChangeSpriteOfButton(eq3, EqUnselected);
                break;
            case "Eq3":
                currentEq= GameData.eq.eq3;
                ChangeSpriteOfButton(eq1, EqUnselected);
                ChangeSpriteOfButton(eq2, EqUnselected);
                ChangeSpriteOfButton(eq3, EqSelected);
                break;
        }
    }
    public void SliderOnValueChanged(Slider slider)
    {
        currentDiff = (GameData.difficulty)slider.value;
    }
    public void InputFieldOnValueChanged(TMP_InputField inputField)
    {
        UserName = inputField.text;
    }
    public void StartGameOnClick()
    {
        PlayerPrefs.SetString("UserName", UserName);
        PlayerPrefs.SetInt("Char", (int)currentChar);
        PlayerPrefs.SetInt("Weapon", (int)currentWeapon);
        PlayerPrefs.SetInt("Eq", (int)currentEq);
        PlayerPrefs.SetInt("Diff", (int)currentDiff);
        SceneManager.LoadScene("GameScene");
    }
    public void SaveUserSettings()
    {
        SaveGame.SaveUserSettings(this);
    }   
    public void LoadUserSettings()
    {
        GameData data = SaveGame.LoadUserSettings();
        if (data != null) 
        {
            UserName = data.UserName;
            currentChar = data.currentChar;
            currentWeapon = data.currentWeapon;
            currentEq = data.currentEq;
            currentDiff = data.currentDiff;
            Difficulty.value = (int)currentDiff;
            inputField.text = UserName;
            switch (currentChar)
            {
                case GameData.character.char1:
                    CharButtonOnClick(char1);
                    break;
                case GameData.character.char2:
                    CharButtonOnClick(char2);
                    break;
                case GameData.character.char3:
                    CharButtonOnClick(char3);
                    break;
            }   
            switch (currentWeapon)
            {
                case GameData.weapon.weapon1:
                    WeaponButtonOnClick(weapon1);
                    break;
                case GameData.weapon.weapon2:
                    WeaponButtonOnClick(weapon2);
                    break;
                case GameData.weapon.weapon3:
                    WeaponButtonOnClick(weapon3);
                    break;
            }
            switch (currentEq)
            {
                case GameData.eq.eq1:
                    EqButtonOnClick(eq1);
                    break;
                case GameData.eq.eq2:
                    EqButtonOnClick(eq2);
                    break;
                case GameData.eq.eq3:
                    EqButtonOnClick(eq3);
                    break;
            }    
        }
        
    }
}
