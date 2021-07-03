using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedButtonAssigner : MonoBehaviourPun
{
    [SerializeField] FixedButton[] fixedButtons;
    [SerializeField] FixedButton[] fixedButtonsList = new FixedButton[7];
    [SerializeField] Message msg;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject gameUI;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        fixedButtons = FindObjectsOfType<FixedButton>();
        msg=FindObjectOfType<Message>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        inventory = FindObjectOfType<Inventory>();
            foreach (FixedButton f in fixedButtons)
            {
                if (f.name == "JumpButton")
                {
                    fixedButtonsList[0] = f;
                }
                if (f.name == "CrouchButton")
                {
                    fixedButtonsList[1] = f;
                }
                if (f.name == "LogoutButton")
                {
                    fixedButtonsList[2] = f;
                }
                if (f.name == "Attack01Button")
                {
                    fixedButtonsList[3] = f;
                }
                if (f.name == "Attack02Button")
                {
                    fixedButtonsList[4] = f;
                }
                if (f.name == "Attack03Button")
                {
                    fixedButtonsList[5] = f;
                }
                if (f.name == "InventoryButton")
                {
                    fixedButtonsList[6] = f;
                }
        }
    }
    public FixedButton[] GetFixedButtons()
    {
        return fixedButtonsList;
    }
    public Message GetMessageBox()
    {
        return msg;
    }
    public Inventory GetInventory()
    {
        return inventory;
    }

    public GameObject GetGameUI()
    {
        return gameUI;
    }
}
