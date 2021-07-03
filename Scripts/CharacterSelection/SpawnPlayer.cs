//using Firebase.Auth;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SpawnPlayer : MonoBehaviour
//{
//    [SerializeField] Object[] characters;
//    [SerializeField] PlayerController character;
//    Vector3 spawnPos;
//    int currentScene;
//    [SerializeField] SaveLoadManager slManager;
//    PlayerStats playerStats;
//    private void Awake()
//    {
//        currentScene = SceneManager.GetActiveScene().buildIndex;
//        playerStats = new PlayerStats();
//    }
//    void Start()
//    {
//        slManager.UpdateSceneToLoad(FirebaseAuth.DefaultInstance.CurrentUser.UserId, 
//            currentScene);
//        characters = Resources.LoadAll("Characters", typeof(PlayerController));
//        character = (PlayerController)characters[playerStats.SelectedPlayer];
//        spawnPos = new Vector3(Random.Range(-40, 400), 2, Random.Range(-40, 400));
//        Instantiate(character, spawnPos, character.transform.rotation);
        
//    }
//    public void onBackClick()
//    {
//        SceneManager.LoadScene(currentScene-1);
//    }
//}
