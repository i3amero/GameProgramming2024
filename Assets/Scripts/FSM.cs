using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum EnemyState {Idle, GotoBase, ChasePlayer}
    public EnemyState currentState;
    public Transform target;               // ������ Ÿ�� (�÷��̾� ��)
    public float chaseDistance = 10f;      // ���� ���� �Ÿ�
    public float attackDistance = 2f;      // ���� ���� �Ÿ�
    public float moveSpeed = 3f;           // �̵� �ӵ�
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 5f;  // �̵��� �� �ε巴�� ����� ���� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.Idle) { Idle(); }
        if (currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        if (currentState == EnemyState.GotoBase) { GotoBase(); }
    }
    private void Idle()
    {
        // Ÿ�ٰ��� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ���� �Ÿ� ������ Ÿ���� ������ Chase ���·� ��ȯ
        if (distanceToTarget <= chaseDistance)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ChasePlayer() 
    {
        // Ÿ�ٰ��� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Ÿ�� �������� ���� ���� õõ�� �̵�
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Acceleration);

        // �ʹ� �־����� �ٽ� Idle ���·� ��ȯ
        if (distanceToTarget > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }
    void GotoBase() { print("GotoBase"); }
}
