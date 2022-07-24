using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MCC = movementcharcontrol
public class MCC : MonoBehaviour
{
    //component
    private CharacterController _charController;
    private ManagerJoystick _mngJoystick;

    //move 
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float moveSpeed;

    void Start()
    {
        moveSpeed = .1f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _charController = tempPlayer.GetComponent<CharacterController>() ;
        _mngJoystick = GameObject.Find("LeftJoystickBG").GetComponent<ManagerJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputX = Input.GetAxis("Horizontal");
        //inputZ = Input.GetAxis("Vertical");
        inputX = _mngJoystick.inputHorizontal();
        inputZ = _mngJoystick.inputVertical();
    }

    private void FixedUpdate() {
        v_movement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
        _charController.Move(v_movement);
    }
}
