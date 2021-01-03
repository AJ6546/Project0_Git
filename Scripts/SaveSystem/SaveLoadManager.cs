using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    PlayerData data;
    DatabaseReference reference;
    PlayerStats playerStats;
    int activeScene;
    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex+1;
    }
    public void SaveData(string userid, string usermail)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        data = new PlayerData(userid, usermail,activeScene,0);
        string jsonData = JsonUtility.ToJson(data);
        reference.Child("Player_"+ userid).SetRawJsonValueAsync(jsonData);
        playerStats = new PlayerStats(userid, usermail, 
            activeScene, 0);
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
                    playerStats = new PlayerStats(userid, pd._email, 
                        pd._sceneToLoad, pd._selectedPlayer);
                }
            }
            );
    }
    public void UpdateSelectedPlayer(string userId,int selectedPlayer)
    {
        playerStats = new PlayerStats();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        IDictionary<string, object> update = new Dictionary<string, object>();
        update["_selectedPlayer"] = selectedPlayer;
        reference.Child("Player_" + userId).UpdateChildrenAsync(update);
        playerStats.SelectedPlayer = selectedPlayer;
    }
    public void UpdateSceneToLoad(string userId, int sceneToLoad)
    {
        playerStats = new PlayerStats();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        IDictionary<string, object> update = new Dictionary<string, object>();
        update["_sceneToLoad"] = sceneToLoad;
        reference.Child("Player_" + userId).UpdateChildrenAsync(update);
        playerStats.SceneToLoad = sceneToLoad;
    }
}
