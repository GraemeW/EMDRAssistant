using UnityEngine;
using EMDR.Core;

namespace EMDR.Saving
{
    [RequireComponent(typeof(MenuController))]
    public class Saver : MonoBehaviour
    {
        // Cached References
        private MenuController menuController;

        #region UnityMethods
        private void Awake()
        {
            menuController = GetComponent<MenuController>();
        }

        private void OnEnable()
        {
            menuController.backgroundColorUpdated += SetBackgroundColorVolatile;
            menuController.windowFullScreenChanged += SetFullScreen;
            menuController.bobbleCosmeticsUpdated += SetBobbleCosmeticsVolatile;
            menuController.bobbleMotionTunablesUpdated +=  SetBobbleMotionTunablesVolatile;
            menuController.settingsUpdateFinished += SaveVolatileToDisk;
        }

        private void OnDisable()
        {
            menuController.backgroundColorUpdated -= SetBackgroundColorVolatile;
            menuController.windowFullScreenChanged -= SetFullScreen;
            menuController.bobbleCosmeticsUpdated -= SetBobbleCosmeticsVolatile;
            menuController.bobbleMotionTunablesUpdated -=  SetBobbleMotionTunablesVolatile;
            menuController.settingsUpdateFinished -= SaveVolatileToDisk;
        }

        private void Start()
        {
            LoadBackgroundSettings();
            LoadBobbleCosmeticSettings();
            LoadMotionSettings();
        }

        private void OnDestroy()
        {
            SaveVolatileToDisk();
        }
        #endregion

        #region LoadState

        private void LoadBackgroundSettings()
        {
            float redBackground = PlayerPrefsInterface.BackgroundColorRedExists() ? PlayerPrefsInterface.GetBackgroundColorRed() : 0f;
            float greenBackground = PlayerPrefsInterface.BackgroundColorGreenExists() ? PlayerPrefsInterface.GetBackgroundColorGreen() : 0f;
            float blueBackground = PlayerPrefsInterface.BackgroundColorBlueExists() ? PlayerPrefsInterface.GetBackgroundColorBlue() : 0f;
            if (menuController != null) { menuController.SetBackgroundColor(new Color(redBackground, greenBackground, blueBackground)); }
            
            if (PlayerPrefsInterface.FullScreenKeyExists()) { Screen.fullScreen = PlayerPrefsInterface.GetFullScreen(); }
        }
        
        private void LoadBobbleCosmeticSettings()
        {
            if (PlayerPrefsInterface.BobbleSizeKeyExists()) { menuController.SetBobbleSize(PlayerPrefsInterface.GetBobbleSize()); }
            if (PlayerPrefsInterface.BobbleTypeKeyExists()) { menuController.SetBobbleShape(PlayerPrefsInterface.GetBobbleType()); }

            float redBobble = PlayerPrefsInterface.BobbleColorRedKeyExists() ? PlayerPrefsInterface.GetBobbleColorRed() : 1f;
            float greenBobble = PlayerPrefsInterface.BobbleColorGreenKeyExists() ? PlayerPrefsInterface.GetBobbleColorGreen() : 1f;
            float blueBobble = PlayerPrefsInterface.BobbleColorBlueKeyExists() ? PlayerPrefsInterface.GetBobbleColorBlue() : 1f;
            menuController.SetBobbleColor(new Color(redBobble, greenBobble, blueBobble));
        }
        
        private void LoadMotionSettings()
        {
            if (PlayerPrefsInterface.BobbleSpeedKeyExists()) { menuController.SetBobbleSpeed(PlayerPrefsInterface.GetBobbleSpeed()); }
            if (PlayerPrefsInterface.BobbleRangeKeyExists()) { menuController.SetBobbleRange(PlayerPrefsInterface.GetBobbleRange()); }
        }
        #endregion

        #region SaveState
        private static void SaveVolatileToDisk()
        {
            PlayerPrefsInterface.SaveToDisk();
        }
        
        private void SetBackgroundColorVolatile(Color color)
        {
            PlayerPrefsInterface.SetBackgroundColorRed(color.r);
            PlayerPrefsInterface.SetBackgroundColorGreen(color.g);
            PlayerPrefsInterface.SetBackgroundColorBlue(color.b);
        }

        private void SetFullScreen(bool isFullScreen)
        {
            PlayerPrefsInterface.SetFullScreen(isFullScreen);
        }

        private void SetBobbleCosmeticsVolatile(BobbleCosmeticData bobbleCosmeticData)
        {
            switch (bobbleCosmeticData.type)
            {
                case BobbleCosmeticDataType.Shape:
                    PlayerPrefsInterface.SetBobbleShape(bobbleCosmeticData.bobbleShape);
                    break;
                case BobbleCosmeticDataType.Size:
                    PlayerPrefsInterface.SetBobbleSize(bobbleCosmeticData.bobbleSize);
                    break;
                case BobbleCosmeticDataType.Color:
                    PlayerPrefsInterface.SetBobbleColorRed(bobbleCosmeticData.bobbleColor.r);
                    PlayerPrefsInterface.SetBobbleColorGreen(bobbleCosmeticData.bobbleColor.g);
                    PlayerPrefsInterface.SetBobbleColorBlue(bobbleCosmeticData.bobbleColor.b);
                    break;
            }
        }

        private void SetBobbleMotionTunablesVolatile(BobbleMotionData bobbleMotionData)
        {
            switch (bobbleMotionData.type)
            {
                case BobbleMotionDataType.Speed:
                    PlayerPrefsInterface.SetBobbleSpeed(bobbleMotionData.relativeSpeed);
                    break;
                case BobbleMotionDataType.Range:
                    PlayerPrefsInterface.SetBobbleRange(bobbleMotionData.xRange);
                    break;
            }
        }
        #endregion
    }
}
