using System.Collections;
using UnityEngine;
using TMPro;

public class DeadManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int seconsToIncrementGems = 180;

    [Header("References")]
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private TextMeshProUGUI gemTextToRevive;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private TimerAndCoin timerAndCoin;
    [SerializeField] private GameObject[] objetsToDesactive;

    [Header("Sound")]
    [SerializeField] private AudioSource positiveButton;
    [SerializeField] private AudioSource negativeButton;

    private GameManager m_GameManager;
    private ShipSpawner m_ShipSpawner;

    private void Start()
    {
        m_GameManager = GameManager.Instance;
        m_ShipSpawner = GetComponent<ShipSpawner>();

        gemTextToRevive.text = GetNeededGems().ToString();
    }

    private int GetNeededGems() => 1 + (m_GameManager.GetPlayerData().time / seconsToIncrementGems);

    public IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.3f);

        Time.timeScale = 0f;
        timerAndCoin.Pause();
        loseMenu.SetActive(true);

        for (int i = 0; i < objetsToDesactive.Length; i++)
            objetsToDesactive[i].SetActive(false);
    }

    public void Revive()
    {
        var currentGems = m_GameManager.GetPlayerData().gems;
        var neededGems = GetNeededGems();

        if (currentGems >= neededGems)
        {
            positiveButton.Play();

            m_ShipSpawner.InstanceShip();
            m_ShipSpawner.WasRevive = true;

            m_GameManager.SetGems(currentGems - neededGems);
            m_GameManager.SavePlayerData();

            Time.timeScale = 1f;
            timerAndCoin.Continue();

            loseMenu.SetActive(false);

            for (int i = 0; i < objetsToDesactive.Length; i++)
                objetsToDesactive[i].SetActive(true);
        }
        else
        {
            negativeButton.Play();
            StartCoroutine(WaitTimeCoroutine(50, messageObject));
        }
    }

    private IEnumerator WaitTimeCoroutine(float duration, GameObject @object)
    {
        @object.SetActive(true);
        yield return new WaitUntil(() => Time.realtimeSinceStartup >= duration);
        @object.SetActive(false);
    }
}
