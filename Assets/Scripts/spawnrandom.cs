using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spawnrandom : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject enemy;
    public int a = 0;
    public GameObject win;

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
        if (a == 10)
        {
            Debug.Log("����Ŭ����");
            win.SetActive(true);
            a = 0;
        }
    }
}
