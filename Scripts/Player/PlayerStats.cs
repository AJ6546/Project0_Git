using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    static string USERID = "USERID", USERMAIL = "USERMAIL";
    static int SCENETOLOAD=0,SELECTEDPLAYER = 0;
    public PlayerStats(string useid, string usermail,int sceneToLoad, int selectedPlayer)
    {
        USERID = useid;
        USERMAIL = usermail;
        SCENETOLOAD = sceneToLoad;
        SELECTEDPLAYER = selectedPlayer;
    }
    public PlayerStats() { }
    public int SceneToLoad
    {
        get { return SCENETOLOAD; }
        set { SCENETOLOAD = value; }
    }
    public int SelectedPlayer
    {
        get { return SELECTEDPLAYER; }
        set { SELECTEDPLAYER = value; }
    }
}
