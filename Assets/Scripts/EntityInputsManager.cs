using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class EntityInputsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject skillButton;
    [SerializeField] private Image skillIcon;

    #region PrivateValues
    private PauseManager pauseManager;
    private GameManager gameManager;
    private bool connected = false;
    #endregion

    #region Properties
    public Vector2 Move { get; set; }
    public Vector3 Aceleration { get; set; }
    public bool Skill { get; set; }
    public bool ControllerConnected { get => connected; }
    #endregion

    #region Mono
    private void Start()
    {
        gameManager = GameManager.Instance;
        pauseManager = FindObjectOfType<PauseManager>(true);
        SetUIControllers();
    }
    #endregion

    #region Callbacks
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnFire(InputValue value)
    {
        FireInput(value.isPressed);
    }
    #endregion

    #region InputMethods
    public void MoveInput(Vector2 newMoveDirection)
    {
        Move = newMoveDirection;
    }
    public void FireInput(bool newFireValue)
    {
        Skill = newFireValue;
    }
    #endregion

    #region Internal
    public void SetUIControllers()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            connected = Gamepad.current != null;

            if (connected)
            {
                joystick.SetActive(false);
                skillButton.SetActive(true);
                skillIcon.sprite = Resources.Load<Sprite>("Icons/B_Button_Icon");
            }
            else if (!connected && gameManager.GetSettingsData().gyroInUse && !pauseManager.IsPaused)
            {
                joystick.SetActive(false);
                skillButton.SetActive(true);
            }
            else if (!connected && !gameManager.GetSettingsData().gyroInUse && !pauseManager.IsPaused)
            {
                joystick.SetActive(true);
                skillButton.SetActive(true);
            }
        }
    }
    #endregion
}