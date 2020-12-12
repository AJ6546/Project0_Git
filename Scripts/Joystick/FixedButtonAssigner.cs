using UnityEngine;

public class FixedButtonAssigner : MonoBehaviour
{
    [SerializeField] FixedButton[] fixedButtons;
    [SerializeField] FixedButton[] fixedButtonsList = new FixedButton[2];
    private void Awake()
    {
        fixedButtons = FindObjectsOfType<FixedButton>();
        foreach(FixedButton f in fixedButtons)
        {
            if(f.name=="JumpButton")
            {
                fixedButtonsList[0] = f;
            }
            if (f.name == "CrouchButton")
            {
                fixedButtonsList[1] = f;
            }
        }
    }
    public FixedButton[] GetFixedButtons()
    {
        return fixedButtonsList;
    }
}
