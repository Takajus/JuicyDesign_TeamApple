using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = System.Random;

public class BFG : MonoBehaviour
{
    private VisualEffect _visualEffect;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 2f;

    private int _direction = 1;

    protected void Start() 
    {
        Destroy(gameObject, lifeTime);

        _visualEffect = GetComponent<VisualEffect>();

        if(_visualEffect)
            _visualEffect.SetFloat("lifetime", lifeTime);
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += Vector3.forward * (_direction * (speed * Time.deltaTime));
    }

    private void BulletHitEnemy(GameObject enemy)
    {
        EnemyManager.Instance.DestroyEnemyInSameLine(enemy);
        JuicyManager.Instance.DestructionSystem(enemy.gameObject);
        JuicyManager.Instance.PopUpScoreSystem(enemy.gameObject, $"{UnityEngine.Random.Range(40, 50)}");

        SoundManager.Instance.PlaySound("AlienDeath");

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        PlayerController.Instance.SetCanShot();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            BulletHitEnemy(other.gameObject);
        }

        if (other.CompareTag("Shield"))
        {
            DestroyBullet();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            DestroyBullet();
        }
    }
}
