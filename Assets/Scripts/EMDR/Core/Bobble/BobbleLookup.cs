using UnityEngine;

namespace EMDR.Core
{
    public class BobbleLookup : MonoBehaviour
    {
        // Tunables
        [SerializeField] private Sprite circle;
        [SerializeField] private Sprite square;
        [SerializeField] private Sprite triangle;

        public Sprite GetBobbleSprite(BobbleType bobbleType)
        {
            switch (bobbleType)
            {
                case BobbleType.Square:
                    return square;
                case BobbleType.Triangle:
                    return triangle;
                case BobbleType.Circle:
                default:
                    return circle;
                
            }
            
        }
    }
}
