using EMDR.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EMDR.UI
{
    public class TypeSelectorUI : MonoBehaviour
    {
        // Tunables
        [SerializeField] private Button circleButton;
        [SerializeField] private Button squareButton;
        [SerializeField] private Button triangleButton;

        public void Subscribe(BobbleType bobbleType, UnityAction action)
        {
            switch (bobbleType)
            {
                case BobbleType.Circle:
                    if (circleButton != null) { circleButton.onClick.AddListener(action); }
                    break;
                case BobbleType.Square:
                    if (squareButton != null) { squareButton.onClick.AddListener(action); }
                    break;
                case BobbleType.Triangle:
                    if (triangleButton != null) { triangleButton.onClick.AddListener(action); }
                    break;
            }
        }

        public void Unsubscribe()
        {
            if (circleButton != null) { circleButton.onClick.RemoveAllListeners(); }
            if (squareButton != null) { squareButton.onClick.RemoveAllListeners(); }
            if (triangleButton != null) { triangleButton.onClick.RemoveAllListeners(); }
        }
    }
}
