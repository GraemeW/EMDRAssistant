using System;
using UnityEngine;

namespace EMDRCore
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EMDRBobble : MonoBehaviour
    {
        // Tunables
        [Header("Movement Parameters")]
        [SerializeField] [Range(0f, 1f)] private float fixedyFractionalPosition = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float initialxFractionalPosition = 0.65f;
        [SerializeField] [Range(0f, 1f)] private float xRange = 1.0f;
        [SerializeField] [Range(0f, 1f)] private float relativeSpeed = 0.2f;

        [Header("Cosmetic Parameters")] 
        [SerializeField] private Color bobbleColor = Color.white;
        [SerializeField] [Range(0f, 1f)] private float bobbleScale = 0.3f;

        // Constants
        private const float _minFractionalSpeed = 0.005f;
        private const float _maxFractionalSpeed = 0.1f;
        
        // State
        private bool isPaused = false;
        private float currentxFractionalPosition = 0.5f;
        private float direction = 1.0f;
        private float spriteToScreenFraction = 0f;
        
        // Cached References
        private Camera mainCamera;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;

        #region UnityEvents
        private void Awake()
        {
            mainCamera = Camera.main;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            
            MoveToFractionalPosition(initialxFractionalPosition);
            SetSize(bobbleScale);
            SetColor(bobbleColor);
        }

        private void FixedUpdate()
        {
            if (isPaused) { return; }
            
            IncrementPosition();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetSize(bobbleScale);
            SetColor(bobbleColor);
        }
#endif
        #endregion
        
        #region PublicMethods
        public void ResetPosition()
        {
            MoveToFractionalPosition(initialxFractionalPosition);
        }

        public void SetSpeed(float speed)
        {
            relativeSpeed = Mathf.Clamp01(speed);
        }

        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size, size, size);

            if (spriteRenderer == null || spriteRenderer.sprite == null) return;
            spriteToScreenFraction = (spriteRenderer.sprite.rect.width / Screen.width) * bobbleScale;
        }

        public void SetColor(Color color)
        {
            bobbleColor = color;
            
            if (spriteRenderer == null) { return; }
            spriteRenderer.color = bobbleColor;
        }
        #endregion

        #region PrivateMethods
        private void MoveToFractionalPosition(float xFractionalPositon)
        {
            currentxFractionalPosition = xFractionalPositon;
            Vector3 targetWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(currentxFractionalPosition, fixedyFractionalPosition, mainCamera.nearClipPlane));
            
            rb.MovePosition(new Vector2(targetWorldPosition.x, targetWorldPosition.y));
        }
        
        private void IncrementPosition()
        {
            ReconcileDirection();
            
            float nextFractionalDirection = currentxFractionalPosition + direction * GetFractionalSpeed();
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
            float testPosition = currentxFractionalPosition + direction *  GetFractionalSpeed();
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
