using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] FixedButton jumpButton;
    [SerializeField] FixedButton crouchButton;
    [SerializeField] FixedButtonAssigner fba;
    [SerializeField] ThirdPersonUserControl control;
    [SerializeField] FixedTouchField touchField;
    [SerializeField] Camera camera;
    [SerializeField] float cameraAngle, cameraSpeed=0.2f,rotOffset;
    [SerializeField] Vector3 cameraOffset;
    void Start()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        fba = GetComponent<FixedButtonAssigner>();
        jumpButton = fba.GetFixedButtons()[0];
        crouchButton = fba.GetFixedButtons()[1];
        control = GetComponent<ThirdPersonUserControl>();
        touchField = FindObjectOfType<FixedTouchField>();
        camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        control.m_Jump = Input.GetKey("space") || jumpButton.Pressed;
        control.m_Crouch = Input.GetKey("c") || crouchButton.Pressed;
        control.hInput = Input.GetAxis("Horizontal") + fixedJoystick.Horizontal;
        control.vInput = Input.GetAxis("Vertical") + fixedJoystick.Vertical;

        cameraAngle += touchField.TouchDist.x * cameraSpeed;

        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position+Vector3.up* rotOffset - 
            camera.transform.position
            , Vector3.up);
    }
}
