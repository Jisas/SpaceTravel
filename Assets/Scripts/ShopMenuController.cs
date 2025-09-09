using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] private Transform shipParent;

    [Header("Normal UI")]
    [SerializeField] private RectTransform sliderContentParent;
    [SerializeField] private RectTransform sliderIndicatorParent;
    [SerializeField] private GameObject sliderContentPrefab;
    [SerializeField] private GameObject sliderIndicatorArrow;
    [SerializeField] private TextMeshProUGUI shipName;
    [SerializeField] private TextMeshProUGUI shipType;
    [SerializeField] private TextMeshProUGUI shipCost;
    [SerializeField] private Animator normalUIAnimator;

    [Header("World Space UI")]
    [SerializeField] private GameObject linesPrefab;
    [SerializeField] private TextMeshProUGUI shipSpeed;
    [SerializeField] private TextMeshProUGUI shipRecistence;
    [SerializeField] private Animator worldUIAnimator;

    [Header("World Space Objects")]
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject grid;

    [Header("Player Data UI")]
    [SerializeField] private TextMeshProUGUI playerCoins;

    [Header("Others")]
    [SerializeField] private Button flyTestButton;
    [SerializeField] private Button flyTestReturnButton;
    [SerializeField] private GameObject joystick;
    [SerializeField] private Animator cinemachineAnim;

    public delegate void OnRightButtonPress();
    public static event OnRightButtonPress OnChangeRight;
    public delegate void OnLefttButtonPress();
    public static event OnLefttButtonPress OnChangeLeft;

    private ShipsDatabase shipsDB;
    private GameObject ship;
    private GameObject lines;
    private int shipIndex = 0;
    private float spaceBetween;
    private int indicatorIndex = 0;

    // Animation IDs
    private int _animIDFadeInNormalUI;
    private int _animIDFadeOutNormalUI;
    private int _animIDInicialStateWorldUI;
    private int _animIDFadeInWorldUI;
    private int _animIDFadeOutWorldUI;

    public int CurrentShipIndex { get => shipIndex; }

    private void Awake()
    {
        shipsDB = Resources.Load<ShipsDatabase>("Databases/ShipsData");             
    }
    private void Start()
    {
        AssignAnimationIDs();
        AssignListeners();
        InstaceShip(shipIndex);
        SetShipDataToUI(shipIndex);
        StartCoroutine(SetupUI());
    }
    private void OnEnable()
    {
        if (ship != null) ActiveWorldSpaceUI();
    }

    private void AssignAnimationIDs()
    {
        _animIDFadeInNormalUI = Animator.StringToHash("FadeIn");
        _animIDFadeOutNormalUI = Animator.StringToHash("FadeOut");
        _animIDInicialStateWorldUI = Animator.StringToHash("InicialState");
        _animIDFadeInWorldUI = Animator.StringToHash("FadeIn");
        _animIDFadeOutWorldUI = Animator.StringToHash("FadeOut");
    }
    private void AssignListeners()
    {
        flyTestButton.onClick.AddListener(() => FlyTest());
        flyTestReturnButton.onClick.AddListener(() => ReturnToShop());
    }
    private IEnumerator SetupUI()
    {
        playerCoins.text = GameManager.Instance.GetPlayerData().coins.ToString();

        ActiveNormalUI();
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < shipsDB.ships.Count; i++)
            Instantiate(sliderContentPrefab, sliderContentParent);

        sliderContentParent.GetChild(0).GetComponent<Image>().color = Color.yellow;

        Canvas.ForceUpdateCanvases();
        spaceBetween = GetNormalSpaceBetween();
    }
    private void InstaceShip(int index)
    {
        if (ship != null)
        {
            ship = null;
            Destroy(shipParent.GetChild(0).gameObject);
            Destroy(shipParent.GetChild(1).gameObject);
            ship = Instantiate(shipsDB.FindShipInDatabase(index).shipPrefab, shipParent);
        }
        else
        {
            ship = Instantiate(shipsDB.FindShipInDatabase(index).shipPrefab, shipParent);
        }

        SetupShip(ship);
    }
    private void SetupShip(GameObject shipObject)
    {
        ShipCollisionsManager collisionManager = shipObject.GetComponent<ShipCollisionsManager>();
        collisionManager.enabled = false;

        UILinesManager.ResetEvents();
        UILinesManager.OnFinishAnimation += () => ActiveWorldSpaceUI();
        lines = Instantiate(linesPrefab, shipParent);

        for (int i = 0; i < 6; i++)
            shipObject.transform.GetChild(i).gameObject.SetActive(false);
    }
    private void SetShipDataToUI(int index)
    {
        shipName.text = shipsDB.FindShipInDatabase(index).name;
        shipType.text = $"{shipsDB.FindShipInDatabase(index).type} ship";
        shipCost.text = $"Cost: {shipsDB.FindShipInDatabase(index).cost}";
        shipSpeed.text = $"{shipsDB.FindShipInDatabase(index).stats.speed} km/s";
        shipRecistence.text = $"{shipsDB.FindShipInDatabase(index).stats.strength} hits";
    }

    private IEnumerator ChangeShipRight()
    {
        shipIndex = shipIndex >= shipsDB.ships.Count ? 0 : (shipIndex + 1) % shipsDB.ships.Count;
        SetShipDataToUI(shipIndex);
        InstaceShip(shipIndex);

        yield return new WaitForEndOfFrame();
        var rectTransform = sliderIndicatorArrow.GetComponent<RectTransform>();
        var currentXPos = rectTransform.localPosition.x;

        if (indicatorIndex >= shipsDB.ships.Count - 1)
        {
            indicatorIndex = 0;
            spaceBetween *= (shipsDB.ships.Count - 1);
            rectTransform.localPosition = new Vector2(currentXPos - spaceBetween, rectTransform.localPosition.y);
        }
        else
        {
            indicatorIndex = (indicatorIndex + 1) % shipsDB.ships.Count;
            spaceBetween = GetNormalSpaceBetween();
            rectTransform.localPosition = new Vector2(currentXPos + spaceBetween, rectTransform.localPosition.y);
        }

        spaceBetween = GetNormalSpaceBetween();
        SetIndicatorColor();
        PlayInicialStateToWorldUI();
        OnChangeRight.Invoke();
    }
    private IEnumerator ChangeShipLeft()
    {
        shipIndex = shipIndex <= 0 ? shipsDB.ships.Count - 1 : (shipIndex - 1) % shipsDB.ships.Count;
        SetShipDataToUI(shipIndex);
        InstaceShip(shipIndex);

        yield return new WaitForEndOfFrame();
        var rectTransform = sliderIndicatorArrow.GetComponent<RectTransform>();
        var currentXPos = rectTransform.localPosition.x;

        if (indicatorIndex <= 0)
        {
            indicatorIndex = shipsDB.ships.Count - 1;
            spaceBetween *= (shipsDB.ships.Count - 1);
            rectTransform.localPosition = new Vector2(currentXPos + spaceBetween, rectTransform.localPosition.y);
        }
        else
        {
            indicatorIndex = (indicatorIndex - 1) % shipsDB.ships.Count;
            spaceBetween = GetNormalSpaceBetween();
            rectTransform.localPosition = new Vector2(currentXPos - spaceBetween, rectTransform.localPosition.y);
        }

        spaceBetween = GetNormalSpaceBetween();
        SetIndicatorColor();
        PlayInicialStateToWorldUI();
        OnChangeLeft.Invoke();
    }
    private IEnumerator FlyTestCoroutine()
    {
        DesactiveWorldSpaceUI();
        yield return new WaitForSeconds(.5f);

        lines.SetActive(false);
        sliderContentParent.gameObject.SetActive(false);
        plane.SetActive(false);
        grid.SetActive(false);
        DesactiveNormalUI();
        yield return new WaitForSeconds(1);

        cinemachineAnim.Play("GameViewState", 0, 0);
        yield return new WaitForSeconds(1f);

        flyTestReturnButton.gameObject.SetActive(true);
        joystick.SetActive(true);
        shipParent.GetComponent<ShipController>().enabled = true;
    }
    private IEnumerator ReturnToShopCoroutine()
    {
        shipParent.localPosition = Vector3.zero;
        shipParent.GetComponent<ShipController>().enabled = false;
        flyTestReturnButton.gameObject.SetActive(false);
        joystick.SetActive(false);
        yield return new WaitForSeconds(.3f);

        cinemachineAnim.Play("BaseState", 0, 0);
        yield return new WaitForSeconds(1);

        plane.SetActive(true);
        grid.SetActive(true);
        lines.gameObject.SetActive(true);
        ActiveWorldSpaceUI();
        yield return new WaitForSeconds(.5f);

        sliderContentParent.gameObject.SetActive(true);
        ActiveNormalUI();
    }

    private float GetNormalSpaceBetween()
    {
        return sliderContentParent.transform.GetChild(1).transform.localPosition.x -
               sliderContentParent.transform.GetChild(0).transform.localPosition.x;
    }
    private void SetIndicatorColor()
    {
        for (int i = 0; i < sliderContentParent.childCount; i++)
            sliderContentParent.GetChild(i).GetComponent<Image>().color = Color.white;

        sliderContentParent.GetChild(indicatorIndex).GetComponent<Image>().color = Color.yellow;
    }
    private void ActiveNormalUI() => normalUIAnimator.Play(_animIDFadeInNormalUI, 0, 0);
    private void DesactiveNormalUI() => normalUIAnimator.Play(_animIDFadeOutNormalUI, 0, 0);
    private void PlayInicialStateToWorldUI() => worldUIAnimator.Play(_animIDInicialStateWorldUI, 0, 0);
    private void ActiveWorldSpaceUI() => worldUIAnimator.Play(_animIDFadeInWorldUI, 0, 0);
    private void DesactiveWorldSpaceUI() => worldUIAnimator.Play(_animIDFadeOutWorldUI, 0, 0);

    public void ResetEvents()
    {
        OnChangeRight = null;
        OnChangeLeft = null;
    }
    public void LeftButton() => StartCoroutine(ChangeShipLeft()); // Called by UI button
    public void RightButton() => StartCoroutine(ChangeShipRight()); // Called by UI button
    public void FlyTest() => StartCoroutine(FlyTestCoroutine());
    public void ReturnToShop() => StartCoroutine(ReturnToShopCoroutine());
}