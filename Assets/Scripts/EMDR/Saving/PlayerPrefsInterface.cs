using System;
using UnityEngine;
using EMDR.Core;

namespace EMDR.Saving
{
    public static class PlayerPrefsInterface
    {
        // Keys
        const string _bobbleSizeKey = "bobbleSize";
        const string _bobbleTypeKey = "bobbleType";
        const string _backgroundColorRedKey = "backgroundColorRed";
        const string _backgroundColorGreenKey = "backgroundColorGreen";
        const string _backgroundColorBlueKey = "backgroundColorBlue";
        const string _bobbleColorRedKey = "bobbleColorRed";
        const string _bobbleColorGreenKey = "bobbleColorGreen";
        const string _bobbleColorBlueKey = "bobbleColorBlue";
        const string _fullScreenKey = "fullScreen";
        const string _bobbleSpeedKey = "bobbleSpeed";
        const string _bobbleRangeKey = "bobbleRange";

        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void SaveToDisk()
        {
            PlayerPrefs.Save();
        }
        
        #region ExistenceChecks
        public static bool BobbleSizeKeyExists() => PlayerPrefs.HasKey(_bobbleSizeKey);
        public static bool BobbleTypeKeyExists() => PlayerPrefs.HasKey(_bobbleTypeKey);
        public static bool BackgroundColorRedExists() => PlayerPrefs.HasKey(_backgroundColorRedKey);
        public static bool BackgroundColorGreenExists() => PlayerPrefs.HasKey(_backgroundColorGreenKey);
        public static bool BackgroundColorBlueExists() => PlayerPrefs.HasKey(_backgroundColorBlueKey);
        public static bool BobbleColorRedKeyExists() => PlayerPrefs.HasKey(_bobbleColorRedKey);
        public static bool BobbleColorGreenKeyExists() => PlayerPrefs.HasKey(_bobbleColorGreenKey);
        public static bool BobbleColorBlueKeyExists() => PlayerPrefs.HasKey(_bobbleColorBlueKey);
        public static bool FullScreenKeyExists() => PlayerPrefs.HasKey(_fullScreenKey);
        public static bool BobbleSpeedKeyExists() => PlayerPrefs.HasKey(_bobbleSpeedKey);
        public static bool BobbleRangeKeyExists() => PlayerPrefs.HasKey(_bobbleRangeKey);
        #endregion

        #region Getters
        public static float GetBobbleSize() => PlayerPrefs.GetFloat(_bobbleSizeKey);

        public static BobbleShape GetBobbleType()
        {
            int bobbleType = PlayerPrefs.GetInt(_bobbleTypeKey);
            return Enum.IsDefined(typeof(BobbleShape), bobbleType) ? (BobbleShape)bobbleType : BobbleShape.Circle;
        }
        public static float GetBackgroundColorRed() => PlayerPrefs.GetFloat(_backgroundColorRedKey);
        public static float GetBackgroundColorGreen() => PlayerPrefs.GetFloat(_backgroundColorGreenKey);
        public static float GetBackgroundColorBlue() => PlayerPrefs.GetFloat(_backgroundColorBlueKey);
        public static float GetBobbleColorRed() => PlayerPrefs.GetFloat(_bobbleColorRedKey);
        public static float GetBobbleColorGreen() => PlayerPrefs.GetFloat(_bobbleColorGreenKey);
        public static float GetBobbleColorBlue() => PlayerPrefs.GetFloat(_bobbleColorBlueKey);
        public static bool GetFullScreen() => PlayerPrefs.GetInt(_fullScreenKey) == 1;
        public static float GetBobbleSpeed() => PlayerPrefs.GetFloat(_bobbleSpeedKey);
        public static float GetBobbleRange() => PlayerPrefs.GetFloat(_bobbleRangeKey);
        #endregion
        
        #region Setters
        public static void SetBobbleSize(float value)
        {
            PlayerPrefs.SetFloat(_bobbleSizeKey, value);
        }

        public static void SetBobbleShape(BobbleShape bobbleShape)
        {
            PlayerPrefs.SetInt(_bobbleTypeKey, (int)bobbleShape);
        }

        public static void SetBackgroundColorRed(float value)
        {
            PlayerPrefs.SetFloat(_backgroundColorRedKey, value);
        }

        public static void SetBackgroundColorGreen(float value)
        {
            PlayerPrefs.SetFloat(_backgroundColorGreenKey, value);
        }

        public static void SetBackgroundColorBlue(float value)
        {
            PlayerPrefs.SetFloat(_backgroundColorBlueKey, value);
        }

        public static void SetBobbleColorRed(float value)
        {
            PlayerPrefs.SetFloat(_bobbleColorRedKey, value);
        }

        public static void SetBobbleColorGreen(float value)
        {
            PlayerPrefs.SetFloat(_bobbleColorGreenKey, value);
        }

        public static void SetBobbleColorBlue(float value)
        {
            PlayerPrefs.SetFloat(_bobbleColorBlueKey, value);
        }
        
        public static void SetFullScreen(bool value)
        {
            PlayerPrefs.SetInt(_fullScreenKey, value ? 1 : 0);
        }

        public static void SetBobbleSpeed(float value)
        {
            PlayerPrefs.SetFloat(_bobbleSpeedKey, value);
        }

        public static void SetBobbleRange(float value)
        {
            PlayerPrefs.SetFloat(_bobbleRangeKey, value);
        }
        #endregion
    }
}
