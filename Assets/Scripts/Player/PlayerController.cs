using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController controller;

    public float speed;
    public float jumpSpeed;

    public Transform cameraPivot;

    Vector3 moveDirection;

    float rotX;
    float rotY;

    void Awake() {
        controller = GetComponent<CharacterController>();
        rotX = transform.localEulerAngles.y;
    }

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        } else {
            float jump = moveDirection.y;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            moveDirection.y = jump;
        }
        moveDirection.y -= 20 * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        rotX += Input.GetAxis("Mouse X") * Time.deltaTime * 100 * 5;
        transform.localEulerAngles = new Vector3(0, rotX, 0);

        rotY += Input.GetAxis("Mouse Y") * Time.deltaTime * -100 * 5;
        rotY = Mathf.Clamp(rotY, -80, 80);
        cameraPivot.localEulerAngles = new Vector3(rotY, 0, 0);
    }
}
