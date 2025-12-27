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

        public void SetUp(BobbleType bobbleType, UnityAction action)
        {
            switch (bobbleType)
            {
                case BobbleType.Circle:
                    circleButton.onClick.AddListener(action);
                    break;
                case BobbleType.Square:
                    squareButton.onClick.AddListener(action);
                    break;
                case BobbleType.Triangle:
                    triangleButton.onClick.AddListener(action);
                    break;
            }
        }

        public void SetDown()
        {
            circleButton.onClick.RemoveAllListeners();
            squareButton.onClick.RemoveAllListeners();
            triangleButton.onClick.RemoveAllListeners();
        }
    }
}
