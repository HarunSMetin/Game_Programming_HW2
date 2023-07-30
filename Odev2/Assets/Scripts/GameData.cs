using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public enum character { char1, char2, char3 };
    public enum weapon { weapon1, weapon2, weapon3 };
    public enum eq { eq1, eq2, eq3 };
    public enum difficulty { easy, medium, hard };

    [Header("User Preferences")]
    public string UserName = "Player";
    public character currentChar = character.char1;
    public weapon currentWeapon = weapon.weapon1;
    public eq currentEq = eq.eq1;
    public difficulty currentDiff = difficulty.easy;

    public GameData(UserSettingsManager userSettings)
    {
        UserName = userSettings.UserName;
        currentChar = userSettings.currentChar;
        currentWeapon = userSettings.currentWeapon;
        currentEq = userSettings.currentEq;
        currentDiff = userSettings.currentDiff;
    }

}
