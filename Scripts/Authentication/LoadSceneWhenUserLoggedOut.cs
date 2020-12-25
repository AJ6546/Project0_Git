using Firebase.Auth;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWhenUserLoggedOut : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    void Start()
    {
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
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
