using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum EnemyState {Idle, GotoBase, ChasePlayer}
    public EnemyState currentState;
    public Transform target;               // ������ Ÿ�� (�÷��̾�)
    public Transform basement;               // ������ Ÿ�� (���̽�)
    public float chaseDistance = 10f;      // ���� ���� �Ÿ�
    public float attackDistance = 2f;      // ���� ���� �Ÿ�
    public float moveSpeed = 20f;           // �̵� �ӵ�
    private Rigidbody rb;
    private Animator anim;
    public float Speed1;
    public float Speed2;
    int i = 0;
    public GameObject lose;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.drag = 5f;  // �̵��� �� �ε巴�� ����� ���� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (currentState == EnemyState.Idle) { Idle(); }
        if (currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        if (currentState == EnemyState.GotoBase) { GotoBase(); }
    }
    private void Idle()
    {
        anim.SetBool("idle", true);
        anim.SetBool("run", false);

        if (i == 120)
        {
            Speed1 = Random.Range(-300, 300);
            Speed2 = Random.Range(-300, 300);
            i = 0;
        }
        Vector3 vector3 = new Vector3(1.0f, 0, 0) * Speed1;
        Vector3 vector32 = new Vector3(0, 0, 1.0f) * Speed2;

        rb.AddForce(vector3, ForceMode.Force);
        rb.AddForce(vector32, ForceMode.Force);
        // Ÿ�ٰ��� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        float distanceToTarget2 = Vector3.Distance(transform.position, basement.position);

        // ���� �Ÿ� ������ Ÿ���� ������ Chase ���·� ��ȯ
        if (distanceToTarget <= chaseDistance)
        {
            currentState = EnemyState.ChasePlayer;
        }
        else if (distanceToTarget2 <= chaseDistance)
        {
            currentState = EnemyState.GotoBase;
        }

    }

    void ChasePlayer()
    {
        anim.SetBool("idle", false);
        anim.SetBool("run", true);

        // Ÿ�ٰ��� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Ÿ�� �������� ���� ���� õõ�� �̵�
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Impulse);

        // ���� �÷��̾ ���� ȸ��
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // �ʹ� �־����� �ٽ� Idle ���·� ��ȯ
        if (distanceToTarget > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player1")
        {
            lose.SetActive(true);
        }

        if (other.gameObject.tag == "base")
        {
            lose.SetActive(true);
        }
    }
    void GotoBase() 
    {
        float distanceToTarget2 = Vector3.Distance(transform.position, target.position);

        // Ÿ�� �������� ���� ���� õõ�� �̵�
        Vector3 direction = (basement.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // ���� �÷��̾ ���� ȸ��
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // �ʹ� �־����� �ٽ� Idle ���·� ��ȯ
        if (distanceToTarget2 > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }
}
