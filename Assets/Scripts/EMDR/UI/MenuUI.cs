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
        
        // Cached References
        MenuController menuController;

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

        private void InitializeUI(bool enable)
        {
            if (menuController == null) { return; }

            if (sizeSlider != null)
            {
                sizeSlider.value = menuController.GetBobbleSize();
                if (enable) { sizeSlider.onValueChanged.AddListener(SetSize);}
                else { sizeSlider.onValueChanged.RemoveListener(SetSize); }
            }
        }

        public void SetSize(float sliderValue)
        {
            if (menuController == null) { return; }
            menuController.SetBobbleSize(sliderValue);
        }
    }
}
