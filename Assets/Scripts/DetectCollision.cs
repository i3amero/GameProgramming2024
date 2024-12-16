using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public AudioSource die;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            die.Play();
            Destroy(other.gameObject);
            (GameObject.Find("Plane").GetComponent<spawnrandom>().a)++;
        }
    }

}
