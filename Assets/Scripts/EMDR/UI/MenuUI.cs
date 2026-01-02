using UnityEngine;
using UnityEngine.UI;
using EMDR.Core;

namespace EMDR.UI
{
    public class MenuUI : MonoBehaviour
    {
        // Tunables
        [Header("Hookups")] 
        [SerializeField] private Slider sizeSlider;
        [SerializeField] private TypeSelectorUI typeSelectorUI;
        [SerializeField] private ColorAdjusterUI backgroundColorAdjusterUI;
        [SerializeField] private ColorAdjusterUI bobbleColorAdjusterUI;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Slider rangeSlider;
        [Header("Settings")] 
        [SerializeField] private bool destroyInstanceOnStart = false;
        
        // Cached References
        private MenuController menuController;

        #region UnityMethods
        private void Awake()
        {
            menuController = MenuController.FindMenuController();
        }

        private void OnEnable()
        {
            if (menuController == null) { menuController = MenuController.FindMenuController(); }
            if (menuController == null) { return; }

            menuController.backgroundColorUpdated += HandleBackgroundColorUpdated;
            menuController.windowFullScreenChanged += HandleWindowFullScreenChanged;
            menuController.bobbleCosmeticsUpdated += HandleBobbleCosmeticsUpdated;
            menuController.bobbleMotionTunablesUpdated += HandleBobbleMotionTunablesUpdated;
        }

        private void OnDisable()
        {
            if (menuController == null) { menuController = MenuController.FindMenuController(); }
            if (menuController == null) { return; }
            
            menuController.backgroundColorUpdated -= HandleBackgroundColorUpdated;
            menuController.windowFullScreenChanged -= HandleWindowFullScreenChanged;
            menuController.bobbleCosmeticsUpdated -= HandleBobbleCosmeticsUpdated;
            menuController.bobbleMotionTunablesUpdated -= HandleBobbleMotionTunablesUpdated;
        }

        private void Start()
        {
            if (destroyInstanceOnStart) { Destroy(this.gameObject); }
            InitializeUI(true);
        }

        private void OnDestroy()
        {
            InitializeUI(false);
        }
        #endregion

        #region Setters
        private void SetSize(float sliderValue)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleSize(sliderValue);
        }

        private void SetType(BobbleShape bobbleShape)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleShape(bobbleShape);
        }

        private void SetBackgroundColor(Color color)
        {
            if (menuController == null) { return; }
            menuController.SetBackgroundColor(color);
        }

        private void SetBobbleColor(Color color)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleColor(color);
        }

        private void SetSpeed(float speed)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleSpeed(speed);
        }

        private void SetRange(float range)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleRange(range);
        }

        private void SetFullScreen(bool isFullScreen)
        {
            if (menuController == null) { return; }
            menuController.SetFullScreen(isFullScreen);
        }

        public void Quit()
        {
            Application.Quit();
        }

        #endregion
        
        #region Initialization
        private void InitializeUI(bool enable)
        {
            if (menuController == null) { return; }

            InitializeTypeAdjuster(enable);
            InitializeSizeAdjuster(enable);
            InitializeColorAdjuster(enable, true, bobbleColorAdjusterUI);
            InitializeColorAdjuster(enable, false, backgroundColorAdjusterUI);
            InitializeFullScreenAdjuster();
            InitializeSpeedAdjuster(enable);
            InitializeRangeAdjuster(enable);
        }

        private void InitializeTypeAdjuster(bool enable)
        {
            if (typeSelectorUI == null) return;
            if (enable)
            {
                typeSelectorUI.Subscribe(BobbleShape.Circle, () => SetType(BobbleShape.Circle));
                typeSelectorUI.Subscribe(BobbleShape.Square, () => SetType(BobbleShape.Square));
                typeSelectorUI.Subscribe(BobbleShape.Triangle, () => SetType(BobbleShape.Triangle));
            }
            else { typeSelectorUI.Unsubscribe(); }
        }
        
        private void InitializeSizeAdjuster(bool enable)
        {
            if (sizeSlider == null) return;
            sizeSlider.value = menuController.GetBobbleSize();
            if (enable) { sizeSlider.onValueChanged.AddListener(SetSize);}
            else { sizeSlider.onValueChanged.RemoveListener(SetSize); }
        }

        private void InitializeColorAdjuster(bool enable, bool isSetBobble, ColorAdjusterUI colorAdjusterUI)
        {
            if (colorAdjusterUI == null) { return; }

            if (enable)
            {
                if (isSetBobble)
                {
                    colorAdjusterUI.Set(menuController.GetBobbleColor());
                    colorAdjusterUI.Subscribe(SetBobbleColor);
                }
                else
                {
                    colorAdjusterUI.Set(menuController.GetBackgroundColor());
                    colorAdjusterUI.Subscribe(SetBackgroundColor);
                }
            }
            else
            {
                if (isSetBobble) { colorAdjusterUI.Unsubscribe(SetBobbleColor); }
                else { colorAdjusterUI.Unsubscribe(SetBackgroundColor); }
            }
        }

        private void InitializeFullScreenAdjuster()
        {
            if (fullscreenToggle == null) { return; }
            
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        }
        
        private void InitializeSpeedAdjuster(bool enable)
        {
            if (speedSlider == null) return;
            speedSlider.value = menuController.GetBobbleSpeed();
            if (enable) { speedSlider.onValueChanged.AddListener(SetSpeed); }
            else { speedSlider.onValueChanged.RemoveListener(SetSpeed); }
        }
        
        private void InitializeRangeAdjuster(bool enable)
        {
            if (rangeSlider == null) return;
            rangeSlider.value = menuController.GetBobbleRange();
            if (enable) { rangeSlider.onValueChanged.AddListener(SetRange); }
            else { rangeSlider.onValueChanged.RemoveListener(SetRange); }
        }
        #endregion
        
        #region EventHandlers

        private void HandleBackgroundColorUpdated(Color color)
        {
            backgroundColorAdjusterUI.SetWithoutNotify(color);
        }

        private void HandleWindowFullScreenChanged(bool isFullScreen)
        {
            fullscreenToggle.SetIsOnWithoutNotify(isFullScreen);
        }
        
        private void HandleBobbleCosmeticsUpdated(BobbleCosmeticData bobbleCosmeticData)
        {
            switch (bobbleCosmeticData.type)
            {
                case BobbleCosmeticDataType.Size:
                    sizeSlider.SetValueWithoutNotify(bobbleCosmeticData.bobbleSize);
                    break;
                case BobbleCosmeticDataType.Color:
                    bobbleColorAdjusterUI.SetWithoutNotify(bobbleCosmeticData.bobbleColor);
                    break;
            }
        }

        private void HandleBobbleMotionTunablesUpdated(BobbleMotionData bobbleMotionData)
        {
            switch (bobbleMotionData.type)
            {
                case BobbleMotionDataType.Speed:
                    speedSlider.SetValueWithoutNotify(bobbleMotionData.relativeSpeed);
                    break;
                case BobbleMotionDataType.Range:
                    rangeSlider.SetValueWithoutNotify(bobbleMotionData.xRange);
                    break;
            }
        }
        #endregion
    }
}
