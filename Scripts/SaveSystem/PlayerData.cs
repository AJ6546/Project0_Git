using System;

[Serializable]
public class PlayerData
{
    public string _userid;
    public string _email;
    public int _selectedPlayer;
    public int _sceneToLoad;

    public  PlayerData() {}

    public  PlayerData(string userid, string usermail, int sceneToLoad,int selectedPlayer)
    {
        _userid = userid;
        _email = usermail;
        _sceneToLoad =sceneToLoad;
        _selectedPlayer = selectedPlayer;
    }
}
