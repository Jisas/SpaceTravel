using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public struct ButtonStruct
{
    public Button button;
    public TextMeshProUGUI text;
    public AudioClip positiveAudio;
    public AudioClip negativeAudio;
}

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ButtonStruct useButton;
    [SerializeField] private ButtonStruct buyButton;

    private ShopMenuController shopController;
    private ShipsDatabase shipsDatabase;
    private GameManager m_GameManager;

    private bool IsAdquired { get => m_GameManager.GetPlayerData().adquiredShipsID.Exists(i => Equals(i, shopController.CurrentShipIndex)); }
    private bool InUse { get => Equals(shopController.CurrentShipIndex, m_GameManager.GetPlayerData().shipInUseId); }

    private void Awake()
    {
        shipsDatabase = Resources.Load<ShipsDatabase>("Databases/ShipsData");
        shopController = GetComponent<ShopMenuController>();
        shopController.ResetEvents();
        AssignListeners();
    }
    private void Start()
    {
        m_GameManager = GameManager.Instance;
        VerifyBuyButton();
        VerifyUseButton();
    }

    private void AssignListeners()
    {
        ShopMenuController.OnChangeRight += () => VerifyBuyButton();
        ShopMenuController.OnChangeRight += () => VerifyUseButton();
        ShopMenuController.OnChangeLeft += () => VerifyBuyButton();
        ShopMenuController.OnChangeLeft += () => VerifyUseButton();

        useButton.button.onClick.AddListener(() => UseShip());
        buyButton.button.onClick.AddListener(() => BuyShip());
    }
    private void VerifyBuyButton() => UpdateButtonUI(buyButton, IsAdquired);
    private void VerifyUseButton()
    {
        bool value;
        if (!IsAdquired && InUse || IsAdquired && !InUse) value = false;
        else value = true;

        UpdateButtonUI(useButton, value);
    }
    private void UpdateButtonUI(ButtonStruct buttonStruct, bool value)
    {
        buttonStruct.button.interactable = !value;

        if (buttonStruct.button.interactable == false) buttonStruct.text.alpha = 0.3f;
        else buttonStruct.text.alpha = 1.0f;
    }

    public void BuyShip()
    {
        Vector3 pos = Camera.main.transform.position;
        Ship currentShip = shipsDatabase.FindShipInDatabase(shopController.CurrentShipIndex);

        if (currentShip == null) return;

        if (m_GameManager.GetPlayerData().coins >= currentShip.cost)
        {
            AudioSource.PlayClipAtPoint(buyButton.positiveAudio, pos);

            int newcoinsValue = m_GameManager.GetPlayerData().coins - currentShip.cost;
            m_GameManager.SetCoins(newcoinsValue);
            m_GameManager.AddAdquiredShipToData(shopController.CurrentShipIndex);
            m_GameManager.SavePlayerData();
            VerifyBuyButton();
            VerifyUseButton();          
        }
        else AudioSource.PlayClipAtPoint(buyButton.negativeAudio, pos);
    }
    public void UseShip()
    {
        Vector3 pos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(useButton.positiveAudio, pos);

        m_GameManager.SetShipInUseID(shopController.CurrentShipIndex);
        UpdateButtonUI(useButton, false);
        m_GameManager.SavePlayerData();
        VerifyUseButton();
    }
}
