using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Object[] characters;
    [SerializeField] PlayerController character;
    Vector3 spawnPos;
    [SerializeField] int sceneToLoad;
    SaveLoadManager slManager;
    PlayerStats playerStats;
    void Start()
    {
        playerStats = new PlayerStats();
        slManager = GetComponent<SaveLoadManager>();
        slManager.UpdateSceneToLoad(FirebaseAuth.DefaultInstance.CurrentUser.UserId, 
            SceneManager.GetActiveScene().buildIndex);
        characters = Resources.LoadAll("Characters", typeof(PlayerController));
        character = (PlayerController)characters[playerStats.SelectedPlayer];
        spawnPos = new Vector3(Random.Range(-40, 40), 2, Random.Range(-40, 40));
        Instantiate(character, spawnPos, character.transform.rotation);

    }
    public void onBackClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
