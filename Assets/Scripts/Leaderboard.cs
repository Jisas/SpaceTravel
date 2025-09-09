using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public void ShowRanking()
    {
        Social.ShowLeaderboardUI();
    }

    public void SendRecord(int record)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(record * 1000, "CalveDelLeaderboard", (bool success) =>
            {
                Debug.Log(success ? "Reported score successfully" : "Failed to report score");
            });
        }
        else Debug.Log("User must authenticate first");
    }
}

