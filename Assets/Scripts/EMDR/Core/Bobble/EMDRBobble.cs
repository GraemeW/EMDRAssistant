using UnityEngine;

namespace EMDR.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BobbleLookup))]
    [RequireComponent(typeof(BobbleMover))]
    public class EMDRBobble : MonoBehaviour
    {
        // Tunables / State
        [SerializeField] private BobbleType bobbleType = BobbleType.Circle;
        [SerializeField] private Color bobbleColor = Color.white;
        [SerializeField] [Range(0f, 1f)] private float bobbleScale = 0.3f;
        
        // Constants
        private const float _minScale = 0.05f;
        private const float _maxScale = 1.5f;
        
        // Cached References
        private SpriteRenderer spriteRenderer;
        private BobbleLookup bobbleLookup;
        private BobbleMover bobbleMover;
        
        #region UnityEvents
        private void Awake()
        {
            SetCache();
            SetType(bobbleType);
            SetSize(bobbleScale);
            SetColor(bobbleColor);
        }
        
        private void SetCache()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            bobbleLookup = GetComponent<BobbleLookup>();
            bobbleMover = GetComponent<BobbleMover>();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            SetCache();
            SetType(bobbleType);
            SetSize(bobbleScale);
            SetColor(bobbleColor);
        }
#endif
        #endregion
        
        #region Getters
        public BobbleType GetBobbleType() => bobbleType;
        public float GetSize() => bobbleScale;
        public Color GetColor() => bobbleColor;
        #endregion
        
        #region Setters
        public void SetSize(float setSize)
        {
            bobbleScale = setSize;
            
            float clampedSize = _minScale + Mathf.Clamp01(setSize) * (_maxScale - _minScale);
            transform.localScale = new Vector3(clampedSize, clampedSize, clampedSize);

            if (spriteRenderer == null || spriteRenderer.sprite == null || bobbleMover == null) return;
            bobbleMover.SetSpriteToScreenFraction((spriteRenderer.sprite.rect.width / Screen.width) * bobbleScale);
        }

        public void SetColor(Color setColor)
        {
            bobbleColor = setColor;
            
            if (spriteRenderer == null) { return; }
            spriteRenderer.color = bobbleColor;
        }

        public void SetType(BobbleType setBobbleType)
        {
            bobbleType = setBobbleType;
            
            if (spriteRenderer == null) { return; }
            spriteRenderer.sprite = bobbleLookup.GetBobbleSprite(setBobbleType);
        }
        #endregion
    }
}
