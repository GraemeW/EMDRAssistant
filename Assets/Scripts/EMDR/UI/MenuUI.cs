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
        #endregion
        
        #region Initialization
        private void InitializeUI(bool enable)
        {
            if (menuController == null) { return; }

            InitializeTypeAdjuster(enable);
            InitializeSizeAdjuster(enable);
        }

        private void InitializeTypeAdjuster(bool enable)
        {
            if (typeSelectorUI == null) return;
            if (enable)
            {
                typeSelectorUI.SetUp(BobbleType.Circle, () => SetType(BobbleType.Circle));
                typeSelectorUI.SetUp(BobbleType.Square, () => SetType(BobbleType.Square));
                typeSelectorUI.SetUp(BobbleType.Triangle, () => SetType(BobbleType.Triangle));
            }
            else { typeSelectorUI.SetDown(); }
        }
        
        private void InitializeSizeAdjuster(bool enable)
        {
            if (sizeSlider == null) return;
            sizeSlider.value = menuController.GetBobbleSize();
            if (enable) { sizeSlider.onValueChanged.AddListener(SetSize);}
            else { sizeSlider.onValueChanged.RemoveListener(SetSize); }
        }
        #endregion
    }
}
