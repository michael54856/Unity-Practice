using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;

    public bool lockCursor = true;
    public Transform playerCamera;
    public float mouseSensitivity;
    private float cameraPitch = 0;
    public GameObject myBarrelPivot;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        MouseLookUpdate();
    }

    void MouseLookUpdate()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -15.0f, 15.0f);
        myBarrelPivot.transform.localEulerAngles = new Vector3((float)(cameraPitch - 15.0f), 0, 0);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch; //Rotate along the X axis Vector3.right = (1, 0, 0)
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);//Rotate along the Y axis Vector3.up=(0,1,0)

    }

    void MovementUpdate()
    {
        Vector3 myDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            myDir += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rotated = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
            myDir += rotated;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myDir -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rotated = Quaternion.AngleAxis(90, Vector3.up) * transform.forward;
            myDir += rotated;
        }
        myDir.Normalize();
        rb.velocity = myDir * moveSpeed;
    }
}
