using UnityEngine;

namespace EMDR.Core
{
    public class MenuController : MonoBehaviour
    {
        // Tunables
        [Header("Hookups")]
        [SerializeField] private EMDRBobble emdrBobble;
        [Header("Prefabs")]
        [SerializeField] private GameObject menuUIPrefab;
        
        // Cached References
        private PlayerInput playerInput;
        private Camera mainCamera;
        private BobbleMover bobbleMover;

        #region StaticMethods
        private const string _menuControllerTag = "GameController";   
        private const string _menuUITag = "MenuUI";
        public static MenuController FindMenuController()
        {
            GameObject menuControllerObject = GameObject.FindGameObjectWithTag(_menuControllerTag);
            return menuControllerObject != null ? menuControllerObject.GetComponent<MenuController>() : null;
        }
        #endregion
        
        #region UnityMethods
        private void Awake()
        {
            VerifyUnique();
            
            playerInput = new PlayerInput();
            playerInput.Menus.Execute.performed += _ => HandleUserInput(PlayerInputType.Execute);
            playerInput.Menus.Cancel.performed += _ => HandleUserInput(PlayerInputType.Cancel);
            playerInput.Menus.Option.performed += _ => HandleUserInput(PlayerInputType.Option);
            
            mainCamera = Camera.main;
        }

        private void Start()
        {
            bobbleMover = emdrBobble != null ? emdrBobble.GetComponent<BobbleMover>() : null;
        }

        private void OnEnable()
        {
            playerInput.Menus.Enable();
        }

        private void OnDisable()
        {
            playerInput.Menus.Disable();
        }
        #endregion
        
        #region BobbleInterfaceMethods
        public float GetBobbleSize() => emdrBobble != null ? emdrBobble.GetSize() : 0f;
        public Color GetBobbleColor() => emdrBobble != null ? emdrBobble.GetColor() : Color.white;
        public Color GetBackgroundColor() => mainCamera != null ? mainCamera.backgroundColor : Color.black;
        public float GetBobbleSpeed() => bobbleMover != null ? bobbleMover.GetRelativeSpeed() : 0.2f;
        public float GetBobbleRange() => bobbleMover != null ? bobbleMover.GetRange() : 1.0f;
        
        public void SetBobbleSize(float size)
        {
            if (emdrBobble == null) { return; }
            emdrBobble.SetSize(size);
        }

        public void SetBobbleType(BobbleType bobbleType)
        {
            if (emdrBobble == null) { return; }
            emdrBobble.SetType(bobbleType);
        }

        public void SetBackgroundColor(Color color)
        {
            if (mainCamera == null) { return; }
            mainCamera.backgroundColor = color;
        }
        
        public void SetBobbleColor(Color color)
        {
            if (emdrBobble == null) { return; }
            emdrBobble.SetColor(color);
        }

        public void SetBobbleSpeed(float speed)
        {
            if (bobbleMover == null) { return;  }
            bobbleMover.SetRelativeSpeed(speed);
        }

        public void SetBobbleRange(float range)
        {
            if (bobbleMover == null) { return; }
            bobbleMover.SetXRange(range);
        }
        #endregion

        #region PrivateMethods
        private void HandleUserInput(PlayerInputType playerInputType)
        {
            switch (playerInputType)
            {
                case PlayerInputType.Execute:
                    bobbleMover.ToggleMovement();
                    break;
                case PlayerInputType.Cancel:
                {
                    GameObject existingMenuUI = GameObject.FindGameObjectWithTag(_menuUITag);
                    if (existingMenuUI != null) { Destroy(existingMenuUI); }
                    else { SpawnMenuUI(); }
                    break;
                }
                case PlayerInputType.Option:
                default:
                    break;
            }
        }

        private void SpawnMenuUI()
        {
            GameObject menuUI = Instantiate(menuUIPrefab);
        }
        
        private void VerifyUnique()
        {
            // Singleton Logic
            var menuControllers = FindObjectsByType<MenuController>(FindObjectsSortMode.None);
            if (menuControllers.Length > 1)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}