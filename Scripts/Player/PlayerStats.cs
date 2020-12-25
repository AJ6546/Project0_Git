using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static string USERID = "USERID", USERMAIL = "USERMAIL", SCORE = "SCORE";
    public PlayerStats(string useid, string usermail, string score)
    {
        USERID = useid;
        USERMAIL = usermail;
        SCORE = score;
    }
}
