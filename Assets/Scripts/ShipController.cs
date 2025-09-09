using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public enum RotationAxis { X, Y, Z }

public class ShipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minMaxPositionX = 100f;
    private float speed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float minRotateValue = -20;
    [SerializeField] private float maxRotateValue = 20;
    [SerializeField] private RotationAxis rotationAxis = RotationAxis.Z;
    private float currentRotation = 0f;

    [Header("Scale")]
    [SerializeField] private float scaleSpeed = 0.3f;

    [Header("Object Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("UI References")]
    public TextMeshProUGUI stateText;
    public Image colorImage;
    public Image cooldownImage;
    public BonusCoinManager BonusText;

    // Private values
    private ShipsDatabase m_ShipDatabase;
    private GameManager m_GameManager;
    private EntityInputsManager m_Inputs;
    private BasePlayer _currentPlayer;
    private ShipSkill _currentSkill;
    private Player _currentPlayerState;
    private float _stateStartTime;
    private float _nextSkillTime = 0f;
    private Vector2 _moveInput;
    private bool _skillInUse;

    // Events
    public delegate void StopShoot();
    public event StopShoot OnStopShoot;

    // Properties
    public Ship CurrentShip { get => m_ShipDatabase.FindShipInUse(); }
    public float ScaleSpeed { get => scaleSpeed; set => scaleSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; }
    public bool SkillIsInUse { get => _skillInUse; }
    public BasePlayer CurrentPlayer { get => _currentPlayer;}
    public ObjectPooler Pooler { get => pooler; }

    private void Awake()
    {
        m_ShipDatabase = Resources.Load<ShipsDatabase>("Databases/ShipsData");
    }
    private void Start()
    {
        m_GameManager = GameManager.Instance;
        m_Inputs = GetComponent<EntityInputsManager>();

        var ship = m_ShipDatabase.FindShipInUse();
        speed = ship.stats.speed;

        _currentPlayer = new BasePlayer();
        _currentPlayerState = _currentPlayer;
        _stateStartTime = Time.time;

        switch (ship.type)
        {
            case ShipType.Combat:
                _currentSkill = new CombatManeuver();
                _currentSkill.SetUp(this);
                break;

            case ShipType.Exploration:
                _currentSkill = new EvasiveManeuver();
                break;
        }
    }
    void Update()
    {
        if (SystemInfo.supportsGyroscope && m_GameManager.GetSettingsData().gyroInUse)
        {
            _moveInput = new Vector2(Input.acceleration.x, Input.acceleration.y);
        }
        else _moveInput = m_Inputs.Move;

        if (_moveInput != Vector2.zero)
        {
            // Horizontal movement
            float moveSpeed = speed * _moveInput.magnitude;
            transform.localPosition += moveSpeed * Time.deltaTime * new Vector3(_moveInput.x, 0, 0);

            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, -minMaxPositionX, minMaxPositionX), 
                transform.localPosition.y, 
                transform.localPosition.z);

            // Rotation
            float rotation = _moveInput.x * rotationSpeed * Time.deltaTime;
            currentRotation += rotation;          
        }
        else
        {
            // Rotation
            float rotation = rotationAxis == RotationAxis.X ? Vector3.zero.x * rotationSpeed * Time.deltaTime :
                             rotationAxis == RotationAxis.Y ? Vector3.zero.y * rotationSpeed * Time.deltaTime :
                                                              Vector3.zero.z * rotationSpeed * Time.deltaTime ; 
            currentRotation = rotation;
        }

        // Decorators
        if (_currentPlayerState != null && SceneManager.GetActiveScene().name == "Game")
        {
            _currentPlayerState.Modify(transform, scaleSpeed, transform.localScale);

            // If the current decorator has exceeded its duration, it changes to the default state.
            if (Time.time - _stateStartTime > _currentPlayerState.Duration)
            {
                _currentPlayerState = new Maximization(_currentPlayer);
                _stateStartTime = Time.time;
            }

            colorImage.color = _currentPlayerState.ModifierColor;
            float remainingTime = _currentPlayerState.Duration - (Time.time - _stateStartTime);
            stateText.text = remainingTime.ToString("F0");
        }

        if (m_ShipDatabase.FindShipInUse().type == ShipType.Exploration && _skillInUse)
            return;

        // Rotation
        currentRotation = Mathf.Clamp(currentRotation, minRotateValue, maxRotateValue);

        Vector3 rotationVector = rotationAxis == RotationAxis.X ? new Vector3(-currentRotation, 0, 0) :
                                 rotationAxis == RotationAxis.Y ? new Vector3(0, -currentRotation, 0) :
                                                                  new Vector3(0, 0, -currentRotation) ;

        var targetRotation = Quaternion.Euler(rotationVector);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Skill
        if (m_Inputs.ControllerConnected && m_Inputs.Skill)
            ExecuteSkill();
    }

    public void ApplyModifier(Player powerUp)
    {
        _currentPlayerState = powerUp;
        _stateStartTime = Time.time;
    }

    public void ExecuteSkill() // Called by a button in UI
    {
        var startTime = Time.time;

        if (Time.time > _nextSkillTime && m_ShipDatabase.FindShipInUse().type == ShipType.Combat)
        {
            StartCoroutine(ShootCoroutine(startTime));
            _nextSkillTime = Time.time + CurrentShip.skillGeneralData.cooldown;
            StartCoroutine(CooldownCoroutine());
        }
        else if (Time.time > _nextSkillTime && m_ShipDatabase.FindShipInUse().type == ShipType.Exploration)
        {
            StartCoroutine(EvasionCoroutine(startTime));
            _nextSkillTime = Time.time + CurrentShip.skillGeneralData.cooldown;
            StartCoroutine(CooldownCoroutine());
        }

        m_Inputs.Skill = false;
    }

    private IEnumerator ShootCoroutine(float startTime)
    {
        while (Time.time - startTime < CurrentShip.skillGeneralData.duration)
        {
            _skillInUse = true;
            _currentSkill.Execute(this);
            yield return new WaitForSeconds(CurrentShip.fireRate);
            StartCoroutine(ShootCoroutine(startTime));
        }

        _skillInUse = false;
        OnStopShoot?.Invoke();
    }

    private IEnumerator EvasionCoroutine(float startTime)
    {
        while (Time.time - startTime < CurrentShip.skillGeneralData.duration)
        {
            _skillInUse = true;
            _currentSkill.Execute(this);
            yield return null;
        }

        _skillInUse = false;
    }

    private IEnumerator CooldownCoroutine()
    {
        float elapsed = 0f;
        var cooldownTime = CurrentShip.skillGeneralData.cooldown;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = elapsed / cooldownTime;
            yield return null;
        }

        cooldownImage.fillAmount = 1; 
    }
}