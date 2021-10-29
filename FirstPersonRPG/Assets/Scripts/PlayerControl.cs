using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    //mouse===========
    public bool lockCursor = true;
    public Transform playerCamera;
    public float mouseSensitivity;
    private float cameraPitch = 0;

    //=================

    //move=============
    public float jumpHeight;

    public float moveSpeed;
    //=================

    //gravity and ground
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool onSlope = false;
    public float velocityY = 0;
    public float gravity = 0;

    public float slopeForce;
    public float slopeForceRayLength;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    //==================

    //dash============
    private Vector3 dashDir;
    private bool dashing = false;
    [SerializeField] private float dashTime;
    public float dashMaxCD = 2f;
    public float dashCooldown = 0;
    public float dashSpeed = 10f;
    //=================

    public Animator handAnimator;

    [SerializeField] private PlayerAttack myAttack;

    void Start()
    {
        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    private void Update()
    {
        MouseLookUpdate();
        MovementUpdate();
        if (dashing == true)
        {
            Dash();
        }
    }

    void MouseLookUpdate()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch; //Rotate along the X axis Vector3.right = (1, 0, 0)
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);//Rotate along the Y axis Vector3.up=(0,1,0)

    }
    void MovementUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        if (inputDir == Vector2.zero)
        {
            handAnimator.SetBool("isRunning", false);
        }
        else
        {
            handAnimator.SetBool("isRunning", true);
        }

        onSlope = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeForceRayLength) && isGrounded)
        {
            if (hit.normal != Vector3.up)
            {
                onSlope = true;
            }
        }
        if (inputDir != Vector2.zero && onSlope && velocityY <= 0)//moving and on slope and not jumping
        {
            controller.Move(Vector3.down * slopeForce * Time.deltaTime);
        }

        velocityY += gravity * Time.deltaTime;
        if (isGrounded && velocityY < 0)//falling and touch ground
        {
            velocityY = -3f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocityY = Mathf.Sqrt(jumpHeight * gravity * -2);
        }

        if(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        if(dashCooldown <= 0)
        {
            dashCooldown = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown == 0)
        {
            dashing = true;
            dashCooldown = dashMaxCD;//set cooldown time
            dashTime = 0.25f;//set the dashing time
            if(inputDir == Vector2.zero)
            {
                dashDir = transform.forward;
            }
            else
            {
                dashDir = (transform.right * inputDir.x + transform.forward * inputDir.y);
            }
        }


        Vector3 velocity = (transform.right * inputDir.x + transform.forward * inputDir.y) * moveSpeed + (Vector3.up * velocityY);

        controller.Move(velocity * Time.deltaTime);

    }
    void Dash()
    {
        dashTime -= Time.deltaTime;
        if(dashTime <= 0)
        {
            dashTime = 0;
            dashing = false;
        }
        controller.Move(dashDir * Time.deltaTime * dashSpeed);
    }
}
