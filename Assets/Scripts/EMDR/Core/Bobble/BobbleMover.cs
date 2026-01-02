using UnityEngine;

namespace EMDR.Core
{
    public class BobbleMover : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float fixedYFractionalPosition = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float initialXFractionalPosition = 0.65f;
        [SerializeField] [Range(0f, 1f)] private float relativeSpeedSetPoint = 0.35f;
        [SerializeField] [Range(0f, 1f)] private float xRange = 1.0f;
        
        // Defaults for Reset
        public static readonly float defaultRelativeSpeedSetPoint = 0.35f;
        public static readonly float defaultXRange = 1.0f;
        
        // Constants
        private const float _minFractionalSpeed = 0.005f;
        private const float _maxFractionalSpeed = 0.05f;
        private const float _timeOffsetFactor = 50.0f;
        private const float _speedRampTime = 1.5f;
        
        // State
        private bool isMovingActive = false;
        private float currentXFractionalPosition = 0.5f;
        private float direction = 1.0f;
        private float fractionalSpeedSetPoint = 0.0f;
        private float currentFractionalSpeed = 0.0f;
        private float targetFractionalSpeed = 0.0f;
        private float speedRamp = 0.0f;
        private float spriteToScreenFraction;
        
        // Cached References
        private Camera mainCamera;
        private Rigidbody2D rb;
        
        #region UnityMethods

        private void Awake()
        {
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            MoveToFractionalPosition(initialXFractionalPosition);
            SetRelativeSpeed(relativeSpeedSetPoint, false);
            SetIsMovingActive(true);
        }

        private void Update()
        {
            ReconcileSpeed(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            IncrementPosition(Time.deltaTime);
        }
        #endregion
        
        #region PublicMethods
        public float GetRelativeSpeed() => relativeSpeedSetPoint;
        public float GetRange() => xRange;
        
        public void ToggleMovement()
        {
            SetIsMovingActive(!isMovingActive);
        }
        
        public void SetSpriteToScreenFraction(float setSpriteToScreenFraction)
        {
            spriteToScreenFraction = setSpriteToScreenFraction;
        }

        public void SetRelativeSpeed(float relativeSpeed, bool overrideCurrentSpeed = true)
        {
            relativeSpeedSetPoint = Mathf.Clamp01(relativeSpeed);
            fractionalSpeedSetPoint = _minFractionalSpeed + (_maxFractionalSpeed - _minFractionalSpeed) * relativeSpeedSetPoint;
            targetFractionalSpeed = fractionalSpeedSetPoint;

            if (overrideCurrentSpeed) { currentFractionalSpeed = fractionalSpeedSetPoint; }
        }

        public void SetXRange(float setXRange)
        {
            xRange = Mathf.Clamp01(setXRange);
        }
        #endregion
        
        #region PrivateMethods
        private float NextPosition(float timeOffset) => currentXFractionalPosition + direction * timeOffset * currentFractionalSpeed;
        
        private Vector2 GetFractionalRangeBounds()
        {
            float lowerLimit = Mathf.Clamp01(0.5f - xRange/2) + spriteToScreenFraction / 2;
            float upperLimit = Mathf.Clamp01(0.5f + xRange/2) - spriteToScreenFraction / 2;
            return new Vector2(lowerLimit, upperLimit);
        }
        
        private void SetIsMovingActive(bool enable)
        {
            isMovingActive = enable;
            targetFractionalSpeed = enable ? fractionalSpeedSetPoint : 0f;
            SetSpeedRamp(enable);
        }

        private void SetSpeedRamp(bool isRampingUp)
        {
            speedRamp =  fractionalSpeedSetPoint / _speedRampTime;
            if (!isRampingUp) { speedRamp *= -1.0f; }
        }
        
        private void MoveToFractionalPosition(float xFractionalPosition)
        {
            currentXFractionalPosition = xFractionalPosition;
            Vector3 targetWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(currentXFractionalPosition, fixedYFractionalPosition, mainCamera.nearClipPlane));
            
            rb.MovePosition(new Vector2(targetWorldPosition.x, targetWorldPosition.y));
        }
        
        private void IncrementPosition(float deltaTime)
        {
            Vector2 fractionalRangeBounds = GetFractionalRangeBounds();
            float timeOffset = _timeOffsetFactor * deltaTime;
            
            ReconcileDirection(fractionalRangeBounds, timeOffset);
            float xFractionalPosition = NextPosition(timeOffset);
            
            MoveToFractionalPosition(xFractionalPosition);
        }

        private void ReconcileDirection(Vector2 fractionalRangeBounds, float timeOffset)
        {
            float testPosition = NextPosition(timeOffset);
            
            if (testPosition <= fractionalRangeBounds.x)
            {
                direction = 1.0f;
            }
            else if (testPosition >= fractionalRangeBounds.y)
            {
                direction = -1.0f;
            }
        }

        private void ReconcileSpeed(float deltaTime)
        {
            if (Mathf.Approximately(currentFractionalSpeed, targetFractionalSpeed) || Mathf.Approximately(speedRamp, 0.0f)) { return; }
            
            currentFractionalSpeed += speedRamp * deltaTime;
            switch (speedRamp)
            {
                case < 0.0f when currentFractionalSpeed < targetFractionalSpeed:
                case > 0.0f when currentFractionalSpeed > targetFractionalSpeed:
                    currentFractionalSpeed = targetFractionalSpeed;
                    break;
            }
        }
        #endregion
    }
}
