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
            playerInput = new PlayerInput();

            VerifyUnique();
            
            playerInput.Menus.Execute.performed += _ => HandleUserInput(PlayerInputType.Execute);
            playerInput.Menus.Cancel.performed += _ => HandleUserInput(PlayerInputType.Cancel);
            playerInput.Menus.Option.performed += _ => HandleUserInput(PlayerInputType.Option);
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
        #endregion

        #region PrivateMethods
        private void HandleUserInput(PlayerInputType playerInputType)
        {
            switch (playerInputType)
            {
                case PlayerInputType.Execute:
                    break;
                case PlayerInputType.Cancel:
                {
                    GameObject existingMenuUI = GameObject.FindGameObjectWithTag(_menuUITag);
                    if (existingMenuUI != null) { Destroy(existingMenuUI); }
                    else { SpawnMenuUI(); }
                    break;
                }
                case PlayerInputType.Option:
                    break;
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