using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum EnemyState {Idle, GotoBase, ChasePlayer}
    public EnemyState currentState;
    public Transform target;               // 추적할 타겟 (플레이어 등)
    public float chaseDistance = 10f;      // 추적 시작 거리
    public float attackDistance = 2f;      // 공격 시작 거리
    public float moveSpeed = 3f;           // 이동 속도
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 5f;  // 이동을 더 부드럽게 만들기 위한 저항 설정
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
        // 타겟과의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 일정 거리 안으로 타겟이 들어오면 Chase 상태로 전환
        if (distanceToTarget <= chaseDistance)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ChasePlayer() 
    {
        // 타겟과의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 타겟 방향으로 힘을 가해 천천히 이동
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Acceleration);

        // 너무 멀어지면 다시 Idle 상태로 전환
        if (distanceToTarget > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }
    void GotoBase() { print("GotoBase"); }
}
