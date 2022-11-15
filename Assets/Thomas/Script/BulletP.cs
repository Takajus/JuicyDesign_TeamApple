using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP : MonoBehaviour
{
    public float force = 600f, destroyTime = 1f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Start()
    {
        rb.AddForce(Vector3.forward * force);
        Destroy(gameObject, destroyTime);
    }
}
