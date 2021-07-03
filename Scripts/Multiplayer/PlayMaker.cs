using Firebase.Auth;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMaker : MonoBehaviourPunCallbacks
{
    PhotonView myPhotonView;
    [SerializeField] GameObject lobbyUI, startMatchGameObject, 
        createRoomGameObject, randomRoomGameObject, enterRoomGameObject, exitRoomGameObject;
    [SerializeField] Text room, message;
    [SerializeField] GameObject[] characters;
    Vector3 spawnPos;
    int currentScene;
    [SerializeField] SaveLoadManager slManager;
    void Awake()
    {
        myPhotonView = GetComponent<PhotonView>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        slManager = GetComponent<SaveLoadManager>();
    }
    void Start()
    {
        string name = PlayerStats.USERMAIL.Split('@')[0];
        slManager.UpdateSceneToLoad(FirebaseAuth.DefaultInstance.CurrentUser.UserId,
            currentScene);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    void Update()
    {
        SetButtons();
    }
    #region UI CallBack Methods
    public void JoinRandomRoom()
    {
        message.text = "Searching for available rooms";
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinCustomRoom()
    {
        message.text = "Trying to join Room " + room.text;
        PhotonNetwork.JoinRoom("Room " + room.text);
    }
    public void CreateAndJoinCustomRoom()
    {
        string randomRoomName = "Room " + Random.Range(10001, 20000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        roomOptions.IsVisible = false;
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    public void OnStartMatchButtonClicked()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        myPhotonView.RPC("RPC_StartGame", RpcTarget.AllBufferedViaServer);
        DeactivateAfterSeconds(lobbyUI, 0.2f);
    }
    public void OnBackButtonClicked()
    {
        StartCoroutine(LeaveMatch(true));
    }
    public void OnExitRoomButtonClicked()
    {
        StartCoroutine(LeaveMatch(false));
    }
    public void OnQuitButtonClick()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }
    #endregion
    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string msg)
    {
        message.text = msg;
        CreateAndJoinRoom();
    }
    public override void OnJoinRoomFailed(short returnCode, string msg)
    {
        message.text = "Room not found, Please type a valid room number or create a new room";
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 6)
        {
            message.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + ". Waiting for other players";
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 6)
        {
            message.text = "Maximum players reached. Starting the game";
            StartCoroutine(DeactivateAfterSeconds(lobbyUI, 2.0f));
            myPhotonView.RPC("RPC_StartGame", RpcTarget.AllBufferedViaServer);
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        message.text = newPlayer.NickName + " joined to " +
        PhotonNetwork.CurrentRoom.Name + " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 6)
        {
            message.text = "Maximum players reached. Starting the game";
            StartCoroutine(DeactivateAfterSeconds(lobbyUI, 2.0f));
        }
    }
    #endregion

    #region PrivateMethods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }
    IEnumerator LeaveMatch(bool loadPreviousScene)
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        if (loadPreviousScene) { SceneManager.LoadScene(1); }
    }
    IEnumerator DeactivateAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
    void SetButtons()
    {

        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient)
        {
            startMatchGameObject.SetActive(true);
        }
        else
        {
            startMatchGameObject.SetActive(false);
        }
        if (PhotonNetwork.InRoom)
        {
            createRoomGameObject.SetActive(false);
            randomRoomGameObject.SetActive(false);
            enterRoomGameObject.SetActive(false);
            exitRoomGameObject.SetActive(true);
        }
        else
        {
            createRoomGameObject.SetActive(true);
            randomRoomGameObject.SetActive(true);
            enterRoomGameObject.SetActive(true);
            exitRoomGameObject.SetActive(false);
        }
    }
    [PunRPC]
    private void RPC_StartGame()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Multiplayer.PLAYER_SELECTION_NUMBER,
                out playerSelectionNumber))
            {
                spawnPos = new Vector3(Random.Range(-40, 40), 2, Random.Range(-40, 40));
                PhotonNetwork.Instantiate(characters[(int)playerSelectionNumber].name, spawnPos, Quaternion.identity);
                Debug.Log("Here");
                lobbyUI.SetActive(false);
            }
        }
    }
    #endregion
}
