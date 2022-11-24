using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = System.Random;

public class BFG : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private void Update()
    {
        transform.position += Vector3.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyManager.Instance.DestroyEnemyInSameLine(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
