using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefab1;
    public GameObject shootPoint;
    public GameObject shootPoint2;
    public GameObject shootPoint3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject clone = Instantiate(prefab);
            GameObject clone2 = Instantiate(prefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
            clone2.transform.position = shootPoint2.transform.position;
            clone2.transform.rotation = shootPoint2.transform.rotation;
            Destroy(clone, 1);
            Destroy(clone2, 1);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject clone = Instantiate(prefab1);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
            Destroy(clone, 1);
        }
    }
    
}
