using System;
using System.Collections;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWhenUserAuthenticated : MonoBehaviour
{
    [SerializeField] SaveLoadManager slManager;
    [SerializeField] string userId = "";
    PlayerStats playerStats;
    private void Start()
    {
        playerStats = new PlayerStats();
        slManager = FindObjectOfType<SaveLoadManager>();
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChanged;
        CheckUser();
    }

    private void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChanged;
    }

    private void HandleAuthStateChanged(object sender, EventArgs e)
    {
        CheckUser();
    }

    private void CheckUser()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser!=null)
        {
            userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        slManager.LoadData(userId);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Convert.ToInt32(playerStats.SceneToLoad));
    }
}
