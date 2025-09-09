using UnityEngine;
using TMPro;

public class TimerAndCoin : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Tiempo Inicial en Segundos")] 
    public int startTime;
    [Tooltip("Escala del Tiempo")] [Range(-10.0f, 10.0f)] 
    public float timeScale = 0;

    [Header("UI References")] 
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public BonusCoinManager bonusCoinManager;

    private GameManager m_GameManager;
    private float TimeToShowInSeconds;
    private float frameTimeWithTimeScale = 0f;
    private float TimeScaleOnPause, startTimeScale;
    private bool isPause = false;
    private int currentTimeRecord;
    private int currentCoins;
    private int currentGems;
    private int time = 0;
    private int coin = 0;
    private int gems = 0;

    public int CurrentTime { get => time; }
    public int CurrentCoins { get => coin; }
    public int CurrentGems { get => gems; set => gems = value; }


    void Start()
    {
        m_GameManager = GameManager.Instance;

        currentTimeRecord = m_GameManager.GetPlayerData().time;
        currentCoins = m_GameManager.GetPlayerData().coins;
        currentGems = m_GameManager.GetPlayerData().gems;

        startTimeScale = timeScale;
        TimeToShowInSeconds = startTime;
        UpdateTimer(startTime);
    }

    void Update()
    {
        if(isPause != true)
        {
            frameTimeWithTimeScale = Time.deltaTime * timeScale;
            TimeToShowInSeconds += frameTimeWithTimeScale;
            UpdateTimer(TimeToShowInSeconds);
        }
    }

    public void UpdateTimer(float timeInSeconds)
    {
        //---------------------------Contador de Tiempo---------------------------

        //Asegurar que el tiempo no sea negativo
        if(timeInSeconds < 0) timeInSeconds = 0;

        //Calcular horas, minutos y segundos
        var hours = (int)timeInSeconds / 3600;
        var minutes = (int)((timeInSeconds - hours * 3600) / 60);
        var seconds = (int)timeInSeconds - (hours * 3600 + minutes * 60);

        //Cadena de caracteres en orden
        string timerInText = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        timerText.text = timerInText;

        time = ((int)timeInSeconds % 3600) + (int)Time.deltaTime * (int)timeScale;

        if (time >= currentTimeRecord)
        {
            m_GameManager.SetTime(time);
            m_GameManager.SavePlayerData();
        }

        //---------------------------Contador de Monedas---------------------------

        //Calcular 1 moneda por cada 2 segundos
        coin = (((int)timeInSeconds % 3600) / 2) + bonusCoinManager.BonusCoin;
        m_GameManager.SetCoins(currentCoins + coin);
        m_GameManager.SavePlayerData();

        //Conversion a cadena de caracteres
        string textoCoin = CurrentCoins.ToString("0");
        coinText.text = textoCoin;

        //----------------------------Contador de Gemas-----------------------------

        string textoGem = gems.ToString();
        gemText.text = textoGem;

        m_GameManager.SetGems(currentGems + gems);
        m_GameManager.SavePlayerData();
    }

    public void Pause()
    {
        if(!isPause)
        {
            isPause = true;
            TimeScaleOnPause = timeScale;
            timeScale = 0f;        
        }
    }

    public void Continue()
    {
        if(isPause)
        {
            isPause = false;
            timeScale = TimeScaleOnPause;
        }
    }

    public void Restart()
    {
        isPause = false;
        timeScale = startTimeScale;
        TimeToShowInSeconds = startTime;
        UpdateTimer(TimeToShowInSeconds);
    }
}
