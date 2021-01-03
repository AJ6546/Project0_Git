using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] Button selectButton;
    [SerializeField] Button reselectButton;
    [SerializeField] GameObject[] characterList;
    [SerializeField] int childIndex, sceneToLoad;
    string action = "Select",userId="";
    [SerializeField] SaveLoadManager slManager;
    PlayerStats playerStats;
    void Start()
    {
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        slManager = GetComponent<SaveLoadManager>();
        playerStats = new PlayerStats();

        characterList = new GameObject[transform.childCount];
        childIndex = playerStats.SelectedPlayer;
        reselectButton.gameObject.SetActive(false);
        for (int i=0;i<characterList.Length;i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
            if(i != childIndex)
            {
                characterList[i].SetActive(false);
            }
        }
        slManager.UpdateSceneToLoad(userId,
            SceneManager.GetActiveScene().buildIndex);
        slManager.UpdateSelectedPlayer(userId, childIndex);
    }
    public void NextCharacter()
    {
        characterList[childIndex].SetActive(false);
        childIndex++;
        if(childIndex> characterList.Length-1)
        {
            childIndex = 0;
        }
        characterList[childIndex].SetActive(true);
        slManager.UpdateSelectedPlayer(userId, childIndex);
    }
    public void PreviousCharacter()
    {
        characterList[childIndex].SetActive(false);
        childIndex--;
        if (childIndex <0)
        {
            childIndex = characterList.Length - 1;
        }
        characterList[childIndex].SetActive(true);
        slManager.UpdateSelectedPlayer(userId, childIndex);
    }
    public void onSelectButtonClick()
    {
        if(action.Equals("Confirm"))
        {
            SceneManager.LoadScene(sceneToLoad); }
        reselectButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
        action=selectButton.GetComponentInChildren<Text>().text = "Confirm";
    }
    public void onReselectButtonClick()
    {
        action=selectButton.GetComponentInChildren<Text>().text = "Select";
        nextButton.gameObject.SetActive(true);
        previousButton.gameObject.SetActive(true);
        reselectButton.gameObject.SetActive(false);
    }
}
