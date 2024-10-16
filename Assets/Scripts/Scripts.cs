using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scripts : MonoBehaviour
{

    [Header("Rotate")]
    public float mouseSpeed;
    float yRotation;
    float xRotation;
    float x;
    float y;
    float moveSpeed = 20.0f;
    Rigidbody rb;
    Animator anim;
    public Camera cam;
    GameObject a;
    int cnt;

    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * 2;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed / 2;
            anim.SetBool("isRunning", false);
        }
        Rotate();
        Move();
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation -= mouseY;    // 마우스 Y축 입력에 따라 수직 회전 값을 조정

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 수직 회전 값을 -90도에서 90도 사이로 제한
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        if (x != 0 || y != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
        Vector3 vec = new Vector3(x, 0, y);

        rb.AddRelativeForce(Vector3.Normalize(vec), ForceMode.VelocityChange);

    }

    private void OnTriggerEnter(Collider other)
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 vec = new Vector3(x, 0, y);
        if (other.tag == "trap")
            rb.velocity = new Vector3(1, 0, 1);
        if (other.tag == "fast")
            rb.AddRelativeForce(Vector3.Normalize(vec * 10), ForceMode.Impulse);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fast")
            moveSpeed = moveSpeed / 2;
    }
}