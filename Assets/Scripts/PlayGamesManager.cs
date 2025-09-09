using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour
{
    public static PlayGamesManager Instance;
    private PlayGamesClientConfiguration clientConfiguration;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        ConfigureGPGS();
    }

    public void Start()
    {
        try
        {
            #if UNITY_ANDROID
            SingInToGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
            #endif
        }
        catch (System.Exception exepcion)
        {
            Debug.LogWarning(exepcion);
        }
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }

    internal void SingInToGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) => 
        { 
            if(code == SignInStatus.Success)
            {
                print("Session started successfully");
            }
            else
            {
                print("Authentication failed due to: " + code);
            }
        });
    }
}
