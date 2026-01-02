using System;
using UnityEngine;
using TMPro;

namespace EMDR.UI
{
    public class ColorAdjusterUI : MonoBehaviour
    {
        // Tunables
        [Header("Hookups")]
        [SerializeField] private TMP_InputField redInput;
        [SerializeField] private TMP_InputField greenInput;
        [SerializeField] private TMP_InputField blueInput;
        
        // Events
        public event Action<Color> onColorUpdated;

        public void SetUp(Color color)
        {
            if (redInput != null) { redInput.text = Mathf.RoundToInt(255 * Mathf.Clamp01(color.r)).ToString(); }
            if (greenInput != null) { greenInput.text = Mathf.RoundToInt(255 * Mathf.Clamp01(color.g)).ToString(); }
            if (blueInput != null) { blueInput.text = Mathf.RoundToInt(255 * Mathf.Clamp01(color.b)).ToString(); }
        }

        public void Subscribe(Action<Color> colorUpdateListener)
        {
            onColorUpdated += colorUpdateListener;
            if (redInput != null) { redInput.onValueChanged.AddListener(HandleColorInputUpdate); }
            if (greenInput != null) { greenInput.onValueChanged.AddListener(HandleColorInputUpdate); }
            if (blueInput != null) { blueInput.onValueChanged.AddListener(HandleColorInputUpdate); }
        }

        public void Unsubscribe(Action<Color> colourUpdateListener)
        {
            onColorUpdated -= colourUpdateListener;
            if (redInput != null) { redInput.onValueChanged.RemoveAllListeners(); }
            if (greenInput != null) { greenInput.onValueChanged.RemoveAllListeners(); }
            if (blueInput != null) { blueInput.onValueChanged.RemoveAllListeners(); }
        }

        private void HandleColorInputUpdate(string newValue)
        {
            int red = Mathf.Clamp(int.TryParse(redInput.text, out red) ? red : 0, 0, 255);
            int green = Mathf.Clamp(int.TryParse(greenInput.text, out green) ? green : 0, 0, 255);
            int blue =  Mathf.Clamp(int.TryParse(blueInput.text, out blue) ? blue : 0, 0, 255);
            
            redInput.SetTextWithoutNotify(red.ToString());
            greenInput.SetTextWithoutNotify(green.ToString());
            blueInput.SetTextWithoutNotify(blue.ToString());
            
            onColorUpdated?.Invoke(new Color(red / 255f, green / 255f, blue / 255f));
        }
    }
}
