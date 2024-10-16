using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f;

    public float jumpForce = 10.0f;          // �����ϴ� ��

    private bool isGround = true;           // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    Rigidbody body;                         // ������Ʈ���� RigidBody�� �޾ƿ� ����

    Animator anim;

    float h, v;
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();   // GetComponent�� Ȱ���Ͽ� body�� �ش� ������Ʈ�� Rigidbody�� �־��ش�.
    }

    void FixedUpdate()
    {
        Move();
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
        // �����̽��ٸ� ������(�Ǵ� ������ ������), �׸��� ĳ���Ͱ� ���� �ִٸ�
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // body�� ���� ���Ѵ�(AddForce)
            // AddForce(����, ���� ��� ���� ���ΰ�)
            anim.SetBool("isJumping", true);
            body.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            body.AddForce(Vector3.down, ForceMode.Impulse);

            // ������ ���������Ƿ� isGround�� false�� �ٲ�
            isGround = false;
        }
    }

    // �浹 �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            // isGround�� true�� ����
            isGround = true;
        }
    }
}