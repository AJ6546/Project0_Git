using System;

[Serializable]
public class PlayerData
{
    public  string _userid;
    public  string _email;
    public  int _score;

    public  PlayerData() {}

    public  PlayerData(string userid, string usermail, int score)
    {
        _userid = userid;
        _email = usermail;
        _score = score;
    }
}
