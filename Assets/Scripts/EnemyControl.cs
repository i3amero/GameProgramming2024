using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float Speed1;
    public float Speed2;
    int i = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i == 120)
        {
            Speed1 = Random.Range(-50, 50);
            Speed2 = Random.Range(-50, 50);
            i = 0;
        }

        Vector3 vector3 = new Vector3(1.0f, 0, 0) * Speed1;
        Vector3 vector32 = new Vector3(0, 0, 1.0f) * Speed2;

        rb.AddForce(vector3);
        rb.AddForce(vector32);
    }

}
