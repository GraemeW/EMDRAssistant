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
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Slider rangeSlider;
        
        // Cached References
        MenuController menuController;

        #region UnityMethods
        private void Awake()
        {
            menuController = MenuController.FindMenuController();
        }

        private void Start()
        {
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

        public void SetType(BobbleType bobbleType)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleType(bobbleType);
        }

        public void SetBackgroundColor(Color color)
        {
            if (menuController == null) { return; }
            menuController.SetBackgroundColor(color);
        }
        
        public void SetBobbleColor(Color color)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleColor(color);
        }

        public void SetSpeed(float speed)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleSpeed(speed);
        }

        public void SetRange(float range)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleRange(range);
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
            InitializeSpeedAdjuster(enable);
            InitializeRangeAdjuster(enable);
        }

        private void InitializeTypeAdjuster(bool enable)
        {
            if (typeSelectorUI == null) return;
            if (enable)
            {
                typeSelectorUI.Subscribe(BobbleType.Circle, () => SetType(BobbleType.Circle));
                typeSelectorUI.Subscribe(BobbleType.Square, () => SetType(BobbleType.Square));
                typeSelectorUI.Subscribe(BobbleType.Triangle, () => SetType(BobbleType.Triangle));
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
                    colorAdjusterUI.SetUp(menuController.GetBobbleColor());
                    colorAdjusterUI.Subscribe(SetBobbleColor);
                }
                else
                {
                    colorAdjusterUI.SetUp(menuController.GetBackgroundColor());
                    colorAdjusterUI.Subscribe(SetBackgroundColor);
                }
            }
            else
            {
                if (isSetBobble) { colorAdjusterUI.Unsubscribe(SetBobbleColor); }
                else { colorAdjusterUI.Unsubscribe(SetBackgroundColor); }
            }
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
    }
}
