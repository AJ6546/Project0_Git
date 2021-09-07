using Firebase.Auth;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] UIAssigner uiAssigner;
    [SerializeField] FixedButton jumpButton, crouchButton, logoutButton, inventoryButton,zoomButton;
    
    [SerializeField] FixedTouchField touchField;

    [SerializeField] ThirdPersonUserControl control;

    [SerializeField] Camera camera;
    [SerializeField] Camera miniMap;
    [SerializeField] Slider miniMapSlider, zoomX,zoomY,zoomZ;
    [SerializeField] float cameraAngle, cameraSpeed = 0.2f, rotOffset;
    [SerializeField] Vector3 cameraOffset;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        uiAssigner = FindObjectOfType<UIAssigner>();
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
        if(jumpButton==null || crouchButton==null || logoutButton==null || zoomButton==null||
            miniMapSlider ==null || zoomX==null
             || zoomY == null || zoomZ == null)
        {
            jumpButton = uiAssigner.GetFixedButtons()[0];
            crouchButton = uiAssigner.GetFixedButtons()[1];
            logoutButton = uiAssigner.GetFixedButtons()[2];
            inventoryButton = uiAssigner.GetFixedButtons()[6];
            zoomButton = uiAssigner.GetFixedButtons()[8];
            miniMapSlider = uiAssigner.GetSliders()[0];
            zoomX = uiAssigner.GetSliders()[1]; zoomX.value = cameraOffset.x;
            zoomY = uiAssigner.GetSliders()[2]; zoomY.value = cameraOffset.y;
            zoomZ = uiAssigner.GetSliders()[3]; zoomZ.value = cameraOffset.z;
        }

        miniMap.transform.position = new Vector3(transform.position.x, miniMap.transform.position.y, 
            transform.position.z);
        miniMap.orthographicSize = miniMapSlider.value;
        cameraOffset.x = zoomX.value;
        cameraOffset.y = zoomY.value;
        cameraOffset.z = zoomZ.value;


        bool logout = Input.GetKey("escape") || logoutButton.Pressed;
        cameraAngle += touchField.TouchDist.x * cameraSpeed;

        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position+Vector3.up* rotOffset - 
            camera.transform.position
            , Vector3.up);

        if (Input.GetKeyDown("i") || inventoryButton.Pressed)
        {
            uiAssigner.GetInventoryUI().SetActive(true);
        }

        if (Input.GetKeyDown("z") || zoomButton.Pressed)
        {
            uiAssigner.GetZoomUI().SetActive(true);
        }
        if (logout)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
            control.m_Jump = Input.GetKey("space") || jumpButton.Pressed;
            control.m_Crouch = Input.GetKey("c") || crouchButton.Pressed;
            control.hInput = Input.GetAxis("Horizontal") + fixedJoystick.Horizontal;
            control.vInput = Input.GetAxis("Vertical") + fixedJoystick.Vertical;
    }
}
