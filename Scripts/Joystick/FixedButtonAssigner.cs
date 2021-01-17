using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedButtonAssigner : MonoBehaviourPun
{
    [SerializeField] FixedButton[] fixedButtons;
    [SerializeField] FixedButton[] fixedButtonsList = new FixedButton[3];
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        fixedButtons = FindObjectsOfType<FixedButton>();
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
            }
    }
    public FixedButton[] GetFixedButtons()
    {
        return fixedButtonsList;
    }
}
