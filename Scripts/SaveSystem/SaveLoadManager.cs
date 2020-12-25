using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    PlayerData data;
    DatabaseReference reference;
    PlayerStats playerStats;
    public void SaveData(string userid, string usermail, int score)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        data = new PlayerData(userid, usermail, score);
        string jsonData = JsonUtility.ToJson(data);
        reference.Child("Player_"+ userid).SetRawJsonValueAsync(jsonData);
        playerStats = new PlayerStats(userid, usermail, Convert.ToString(score));
    }
    public void LoadData(string userid)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.GetValueAsync().ContinueWith(
            task =>
            {
                if (task.IsCanceled) { Debug.Log("Loading data canceled"); return; }
                if (task.IsFaulted) { Debug.Log(task.Exception.Flatten().InnerExceptions[0].Message); }
                if (task.IsCompleted)
                {
                    DataSnapshot data = task.Result;
                    string playerData = data.Child("Player_" + userid).GetRawJsonValue();
                    PlayerData pd = JsonUtility.FromJson<PlayerData>(playerData);
                    playerStats = new PlayerStats(userid, pd._email, Convert.ToString(pd._score));
                }
            }
            );
    }
}
