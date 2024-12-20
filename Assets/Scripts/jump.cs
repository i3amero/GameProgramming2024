using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f;

    public float jumpForce = 10.0f;          // 점프하는 힘

    private bool isGround = true;           // 캐릭터가 땅에 있는지 확인할 변수

    Rigidbody body;                         // 컴포넌트에서 RigidBody를 받아올 변수

    Animator anim;

    float h, v;
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();   // GetComponent를 활용하여 body에 해당 오브젝트의 Rigidbody를 넣어준다.
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (!(h == 0 && v == 0))
        {
            transform.position += dir * Speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }

    void Jump()
    {
        // 스페이스바를 누르면(또는 누르고 있으면), 그리고 캐릭터가 땅에 있다면
        if (Input.GetKey(KeyCode.Space) && isGround == true)
        {
            // body에 힘을 가한다(AddForce)
            // AddForce(방향, 힘을 어떻게 가할 것인가)
            anim.SetBool("isJumping", true);
            body.AddForce(Vector3.up * jumpForce * 30, ForceMode.Impulse);

            // 땅에서 떨어졌으므로 isGround를 false로 바꿈
            isGround = false;
            body.AddForce(Vector3.down * jumpForce * 10, ForceMode.Impulse);
        }
    }

    // 충돌 함수
    void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체의 태그가 "Ground"라면
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            // isGround를 true로 변경
            isGround = true;
        }
    }
}