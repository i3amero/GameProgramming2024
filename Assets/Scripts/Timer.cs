using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class T : MonoBehaviour
{
    public float timer = 120;
    public TextMeshProUGUI A;
    public GameObject lose;
    // Start is called before the first frame update
    void Start()
    {
        A.text = $"{timer}"; 
    }

    // Update is called once per frame
    void Update()
    {
        A.text = $"{timer}";
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = 0;
            A.text = $"{timer}";
            Debug.Log("게임 오바");
            lose.SetActive(true);
        }
    }
}
