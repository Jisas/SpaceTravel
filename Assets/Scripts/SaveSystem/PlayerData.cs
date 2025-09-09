using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int gameNum;
    public int time;
    public int coins;
    public int gems;
    public int shipInUseId;
    public List<int> adquiredShipsID;
}

