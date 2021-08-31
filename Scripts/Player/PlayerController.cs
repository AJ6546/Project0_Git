using Firebase.Auth;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] FixedButtonAssigner fba;
    [SerializeField] FixedButton jumpButton;
    [SerializeField] FixedButton crouchButton;
    [SerializeField] FixedButton logoutButton;
    [SerializeField] FixedButton inventoryButton;
    [SerializeField] FixedTouchField touchField;

    [SerializeField] ThirdPersonUserControl control;

    [SerializeField] Camera camera;
    [SerializeField] Camera miniMap;
    [SerializeField] Slider miniMapSlider;
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
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        miniMap= GameObject.FindGameObjectWithTag("Minimap").GetComponent<Camera>();
       
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        if(jumpButton==null || crouchButton==null || logoutButton==null || miniMapSlider==null)
        {
            jumpButton = fba.GetFixedButtons()[0];
            crouchButton = fba.GetFixedButtons()[1];
            logoutButton = fba.GetFixedButtons()[2];
            inventoryButton = fba.GetFixedButtons()[6];
            miniMapSlider = FindObjectOfType<Slider>();
        }

        miniMap.transform.position = new Vector3(transform.position.x, miniMap.transform.position.y, 
            transform.position.z);
        miniMap.orthographicSize = miniMapSlider.value;

        bool logout = Input.GetKey("escape") || logoutButton.Pressed;
        cameraAngle += touchField.TouchDist.x * cameraSpeed;

        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position+Vector3.up* rotOffset - 
            camera.transform.position
            , Vector3.up);

        if (Input.GetKeyDown("i") || inventoryButton.Pressed)
        {
            fba.GetInventory().gameObject.SetActive(true);
            //fba.GetGameUI().SetActive(false);
        }

        if(logout)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
        //if (!health.isDead && !fighter.freeze)
        //{
            control.m_Jump = Input.GetKey("space") || jumpButton.Pressed;
            control.m_Crouch = Input.GetKey("c") || crouchButton.Pressed;
            control.hInput = Input.GetAxis("Horizontal") + fixedJoystick.Horizontal;
            control.vInput = Input.GetAxis("Vertical") + fixedJoystick.Vertical;
        //}
    }
}
