using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum EnemyState {Idle, GotoBase, ChasePlayer}
    public EnemyState currentState;
    public Transform target;               // 추적할 타겟 (플레이어)
    public Transform basement;               // 추적할 타겟 (베이스)
    public float chaseDistance = 10f;      // 추적 시작 거리
    public float attackDistance = 2f;      // 공격 시작 거리
    public float moveSpeed = 20f;           // 이동 속도
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
        rb.drag = 5f;  // 이동을 더 부드럽게 만들기 위한 저항 설정
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
        // 타겟과의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        float distanceToTarget2 = Vector3.Distance(transform.position, basement.position);

        // 일정 거리 안으로 타겟이 들어오면 Chase 상태로 전환
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

        // 타겟과의 거리 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 타겟 방향으로 힘을 가해 천천히 이동
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Impulse);

        // 적이 플레이어를 향해 회전
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // 너무 멀어지면 다시 Idle 상태로 전환
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

        // 타겟 방향으로 힘을 가해 천천히 이동
        Vector3 direction = (basement.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // 적이 플레이어를 향해 회전
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // 너무 멀어지면 다시 Idle 상태로 전환
        if (distanceToTarget2 > chaseDistance)
        {
            currentState = EnemyState.Idle;
        }
    }
}
