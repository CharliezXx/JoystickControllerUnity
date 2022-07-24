using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MCC = movementcharcontrol
public class MCC : MonoBehaviour
{
    //component
    private CharacterController _charController;
    private Animator _animator;
    private ManagerJoystick _mngJoystick;

    //
    private Transform meshPlayer;

    //move 
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float moveSpeed;
    private float gravity;

    void Start()
    {
        moveSpeed = .1f;
        gravity = .5f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = tempPlayer.transform.GetChild(0);
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
        _mngJoystick = GameObject.Find("LeftJoystickBG").GetComponent<ManagerJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputX = Input.GetAxis("Horizontal");
        //inputZ = Input.GetAxis("Vertical");
        inputX = _mngJoystick.inputHorizontal();
        inputZ = _mngJoystick.inputVertical();
        if (inputX == 0 && inputZ == 0)
        {
            _animator.SetBool("isRun", false);
        }
        else
        {
            _animator.SetBool("isRun", true);
        }
    }

    private void FixedUpdate()
    {

        //gravity 
        if (_charController.isGrounded)
        {
            v_movement.y = 0f;
        }
        else
        {
            v_movement.y -= gravity * Time.deltaTime;
        }
        //movement
        v_movement = new Vector3(inputX * moveSpeed, v_movement.y, inputZ * moveSpeed);
        _charController.Move(v_movement);

        //mesh rotate
        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);
        }

    }
}
