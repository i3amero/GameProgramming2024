using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnrandom : MonoBehaviour
{
    // ������ ����� Plane�� �ڽ��� RespawnRange ������Ʈ
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject enemy;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        int i = 0;
        while (i != 10)
        {
            yield return new WaitForSeconds(1f);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(enemy, Return_RandomPosition(), Quaternion.identity);
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
