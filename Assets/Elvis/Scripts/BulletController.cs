using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = System.Random;

public class BulletController : MonoBehaviour
{
    private VisualEffect visualEffect;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 2f;

    protected int _direction = 1;
    
    protected void Start()
    {
        Destroy(gameObject, lifeTime);

        visualEffect = GetComponent<VisualEffect>();

        visualEffect.SetFloat("lifetime", lifeTime);
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += Vector3.forward * (_direction * (speed * Time.deltaTime));
    }
    
    public void SetDirection(int direction)
    {
        _direction = direction;
    }

    private void BulletHitPlayer(GameObject player)
    {
        PlayerController.Instance.GetDamage(1);
        Destroy(gameObject);
    }
    
    private void BulletHitEnemy(GameObject enemy)
    {
        JuicyManager.Instance.DestructionSystem(enemy.gameObject);
        JuicyManager.Instance.PopUpScoreSystem(enemy.gameObject, "13");
                
        SoundManager.Instance.PlaySound("Destruction alien");
        
        Destroy(enemy);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        PlayerController.Instance.SetCanShot();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_direction == 1)
        {
            if (other.CompareTag("Enemy"))
            {
                // EnemyManager.Instance.DestroyEnemyInSameLine(other.gameObject);
                
                BulletHitEnemy(other.gameObject);
                DestroyBullet();
            }
        }
        else
        {
            if (other.CompareTag("Player")) 
            {
                BulletHitPlayer(other.gameObject);
            }
        }
        
        if (other.CompareTag("Bullet") || other.CompareTag("EnemyShield"))
            DestroyBullet();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            switch (_direction)
            {
                case 1:
                    DestroyBullet();
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
