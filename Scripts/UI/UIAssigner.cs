using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAssigner : MonoBehaviourPun
{
    [SerializeField] FixedButton[] fixedButtons;
    [SerializeField] FixedButton[] fixedButtonsList = new FixedButton[9];

    [SerializeField] Slider[] sliders;
    [SerializeField] Slider[] slidersList = new Slider[4];

    [SerializeField] Message msg;

    [SerializeField] GameObject inventoryUI,zoomUI;
    

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        fixedButtons = FindObjectsOfType<FixedButton>();
        sliders = FindObjectsOfType<Slider>();
        msg=FindObjectOfType<Message>();
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        zoomUI = GameObject.FindGameObjectWithTag("ZoomUI");

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
            if (f.name == "UnEquipButton")
            {
                fixedButtonsList[7] = f;
            }
            if (f.name == "ZoomButton")
            {
                fixedButtonsList[8] = f;
            }
        }

        foreach(Slider s in sliders)
        {
            if(s.name=="MinimapSlider")
            {
                slidersList[0] = s;
            }
            if (s.name == "ZoomX")
            {
                slidersList[1] = s;
            }
            if (s.name == "ZoomY")
            {
                slidersList[2] = s;
            }
            if (s.name == "ZoomZ")
            {
                slidersList[3] = s;
            }
        }
    }
    public FixedButton[] GetFixedButtons()
    {
        return fixedButtonsList;
    }
    public Slider[] GetSliders()
    {
        return slidersList;
    }
    public Message GetMessageBox()
    {
        return msg;
    }
    public GameObject GetInventoryUI()
    {
        return inventoryUI;
    }
    public GameObject GetZoomUI()
    {
        return zoomUI;
    }

}
