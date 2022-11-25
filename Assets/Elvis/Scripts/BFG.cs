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
            PlayerController.Instance.SetCanShot();
            Destroy(gameObject);
        }

        if (other.CompareTag("Shield"))
        {
            other.GetComponent<ShieldController>().DestroyShield();
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            PlayerController.Instance.SetCanShot();
            Destroy(gameObject);
        }
    }
}
