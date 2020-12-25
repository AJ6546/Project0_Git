using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] FixedButton jumpButton;
    [SerializeField] FixedButton crouchButton;
    [SerializeField] FixedButton logoutButton;
    [SerializeField] FixedButtonAssigner fba;
    [SerializeField] ThirdPersonUserControl control;
    [SerializeField] FixedTouchField touchField;
    [SerializeField] Camera camera;
    [SerializeField] float cameraAngle, cameraSpeed = 0.2f, rotOffset;
    [SerializeField] Vector3 cameraOffset;
    //[SerializeField] Text txtMsg;

    void Start()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        fba = GetComponent<FixedButtonAssigner>();
        jumpButton = fba.GetFixedButtons()[0];
        crouchButton = fba.GetFixedButtons()[1];
        logoutButton = fba.GetFixedButtons()[2];
        control = GetComponent<ThirdPersonUserControl>();
        touchField = FindObjectOfType<FixedTouchField>();
        camera = FindObjectOfType<Camera>();
        Debug.Log("Player Score =" + PlayerStats.SCORE);
        //txtMsg.text += PlayerStats.SCORE; // Please add a Textbox in Scene and set it up before uncommenting this 
    }
    void Update()
    {
        control.m_Jump = Input.GetKey("space") || jumpButton.Pressed;
        control.m_Crouch = Input.GetKey("c") || crouchButton.Pressed;
        control.hInput = Input.GetAxis("Horizontal") + fixedJoystick.Horizontal;
        control.vInput = Input.GetAxis("Vertical") + fixedJoystick.Vertical;
        bool logout = Input.GetKey("escape") || logoutButton.Pressed;
        cameraAngle += touchField.TouchDist.x * cameraSpeed;

        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position+Vector3.up* rotOffset - 
            camera.transform.position
            , Vector3.up);

        if(logout)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }
}
