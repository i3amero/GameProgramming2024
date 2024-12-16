using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    [Header("Rotate")]
    public float mouseSpeed = 100f;  // 마우스 감도
    private float moveSpeed = 20f;    // 이동 속도
    private float yRotation;
    private Rigidbody rb;
    private Animator anim;
    public CinemachineVirtualCamera cam;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정
        rb.freezeRotation = true;                   // Rigidbody의 회전을 잠금
    }

    void FixedUpdate()
    {
        HandleRunning();
        Rotate();
        Move();
    }

    void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2;
            anim.SetBool("run", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2;
            anim.SetBool("run", false);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = cam.transform.forward * verticalInput + cam.transform.right * horizontalInput;
        moveDirection.y = 0;
        moveDirection.Normalize();

        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("idle", true);
        }

        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y; // 중력 유지
        rb.velocity = velocity;
    }
}
