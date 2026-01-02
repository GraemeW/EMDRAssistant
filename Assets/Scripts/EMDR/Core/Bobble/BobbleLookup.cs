using UnityEngine;

namespace EMDR.Core
{
    public class BobbleLookup : MonoBehaviour
    {
        // Tunables
        [SerializeField] private Sprite circle;
        [SerializeField] private Sprite square;
        [SerializeField] private Sprite triangle;

        public Sprite GetBobbleSprite(BobbleShape bobbleShape)
        {
            switch (bobbleShape)
            {
                case BobbleShape.Square:
                    return square;
                case BobbleShape.Triangle:
                    return triangle;
                case BobbleShape.Circle:
                default:
                    return circle;
                
            }
            
        }
    }
}
