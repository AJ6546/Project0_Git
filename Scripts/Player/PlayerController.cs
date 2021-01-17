using Firebase.Auth;
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPun
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

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        fba = FindObjectOfType<FixedButtonAssigner>();
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        control = GetComponent<ThirdPersonUserControl>();
        touchField = FindObjectOfType<FixedTouchField>();
        camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        if(jumpButton==null || crouchButton==null || logoutButton==null)
        {
            jumpButton = fba.GetFixedButtons()[0];
            crouchButton = fba.GetFixedButtons()[1];
            logoutButton = fba.GetFixedButtons()[2];
        }
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
