using UnityEngine;

namespace EMDR.Core
{
    public class BobbleMover : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float fixedYFractionalPosition = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float initialXFractionalPosition = 0.65f;
        [SerializeField] [Range(0f, 1f)] private float xRange = 1.0f;
        [SerializeField] [Range(0f, 1f)] private float relativeSpeed = 0.2f;
        
        // Constants
        private const float _minFractionalSpeed = 0.005f;
        private const float _maxFractionalSpeed = 0.1f;
        
        // State
        private bool isPaused = false;
        private float currentXFractionalPosition = 0.5f;
        private float direction = 1.0f;
        private float spriteToScreenFraction;
        
        // Cached References
        private Camera mainCamera;
        private Rigidbody2D rb;
        
        #region UnityMethods

        private void Awake()
        {
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            MoveToFractionalPosition(initialXFractionalPosition);
        }

        private void FixedUpdate()
        {
            if (isPaused) { return; }
            
            IncrementPosition();
        }
        #endregion
        
        #region PublicMethods

        public void SetSpriteToScreenFraction(float setSpriteToScreenFraction)
        {
            spriteToScreenFraction = setSpriteToScreenFraction;
        }
        
        
        public void ResetPosition()
        {
            MoveToFractionalPosition(initialXFractionalPosition);
        }

        public void SetSpeed(float speed)
        {
            relativeSpeed = Mathf.Clamp01(speed);
        }
        #endregion
        
        #region PrivateMethods
        private void MoveToFractionalPosition(float xFractionalPositon)
        {
            currentXFractionalPosition = xFractionalPositon;
            Vector3 targetWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(currentXFractionalPosition, fixedYFractionalPosition, mainCamera.nearClipPlane));
            
            rb.MovePosition(new Vector2(targetWorldPosition.x, targetWorldPosition.y));
        }
        
        private void IncrementPosition()
        {
            ReconcileDirection();
            
            float nextFractionalDirection = currentXFractionalPosition + direction * GetFractionalSpeed();
            MoveToFractionalPosition(nextFractionalDirection);
        }

        private Vector2 GetFractionalRangeBounds()
        {
            float lowerLimit = Mathf.Clamp01(0.5f - xRange/2) + spriteToScreenFraction / 2;
            float upperLimit = Mathf.Clamp01(0.5f + xRange/2) - spriteToScreenFraction / 2;
            return new Vector2(lowerLimit, upperLimit);
        }

        private float GetFractionalSpeed()
        {
            return _minFractionalSpeed + (_maxFractionalSpeed - _minFractionalSpeed) * Mathf.Clamp01(relativeSpeed);
        }

        private void ReconcileDirection()
        {
            float testPosition = currentXFractionalPosition + direction *  GetFractionalSpeed();
            Vector2 fractionalRangeBounds = GetFractionalRangeBounds();
            if (testPosition < fractionalRangeBounds.x)
            {
                direction = 1.0f;
            }
            else if (testPosition > fractionalRangeBounds.y)
            {
                direction = -1.0f;
            }
        }
        #endregion
    }
}
