using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovment : MonoBehaviour
    {
    public Transform cam;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 0.5f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVerlocity = 0.1f;
    private Vector3 moveDirection;
    private void Start()
        {
        controller = gameObject.GetComponent<CharacterController>();
        }

    void Update()
        {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
            {
            playerVelocity.y = 0f;
            }

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalAxis, 0, verticalAxis);
        if (moveDirection.magnitude >= 0.01f)
            {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVerlocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir * playerSpeed * Time.deltaTime);
            }
          
        if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        }
    }