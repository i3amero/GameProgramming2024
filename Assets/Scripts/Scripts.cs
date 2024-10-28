using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scripts : MonoBehaviour
{
    [Header("Rotate")]
    public float mouseSpeed = 100f;  // 마우스 감도
    private float moveSpeed = 40f;    // 이동 속도
    private float yRotation;
    private float xRotation;
    private Rigidbody rb;
    private Animator anim;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정
        rb.freezeRotation = true;                   // Rigidbody의 회전을 잠금
    }

    void Update()
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
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2;
            anim.SetBool("isRunning", false);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 캐릭터의 수평 회전 및 카메라의 수직 회전 설정
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 카메라 방향을 기준으로 이동 벡터 설정
        Vector3 moveDirection = cam.transform.forward * verticalInput + cam.transform.right * horizontalInput;
        moveDirection.y = 0; // y축 이동 방지
        moveDirection.Normalize();

        // 애니메이션 설정
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }

        // Rigidbody 속도를 카메라 기준 이동 방향으로 설정
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trap"))
            rb.velocity = new Vector3(1, 0, 1);
        else if (other.CompareTag("fast"))
            moveSpeed *= 2;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("fast"))
            moveSpeed /= 2;
    }
}
