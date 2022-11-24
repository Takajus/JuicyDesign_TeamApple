using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = System.Random;

public class BulletController : MonoBehaviour
{
    private VisualEffect _visualEffect;
    
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 2f;

    protected bool IsEnemyBullet = false;
    protected bool CanBounce = false;
    protected int Direction = 1;
    
    protected void Start()
    {
        Destroy(gameObject, lifeTime);

        _visualEffect = GetComponent<VisualEffect>();

        _visualEffect.SetFloat("lifetime", lifeTime);
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += Vector3.forward * (Direction * (speed * Time.deltaTime));
    }

    public void SetIsEnemyBullet(bool value = false)
    {
        if (!value)
        {
            SetDirection(1);
            IsEnemyBullet = false;
        }
        else
        {
            SetDirection(-1);
            IsEnemyBullet = true;
            CanBounce = true;
        }
        
    }
    
    private void SetDirection(int direction)
    {
        Direction = direction;
    }

    private void BulletHitPlayer(GameObject player)
    {
        PlayerController.Instance.GetDamage(1);
        SoundManager.Instance.PlaySound("PlayerDeath");
        Destroy(gameObject);
    }
    
    private void BulletHitEnemy(GameObject enemy)
    {
        JuicyManager.Instance.DestructionSystem(enemy.gameObject);
        JuicyManager.Instance.PopUpScoreSystem(enemy.gameObject, $"{UnityEngine.Random.Range(10, 20)}");
                
        SoundManager.Instance.PlaySound("AlienDeath");
        
        Destroy(enemy);
        DestroyBullet();
    }

    private void BulletHitShield(GameObject shield)
    {
        shield.GetComponent<ShieldController>().GetHit();
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        PlayerController.Instance.SetCanShot();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!IsEnemyBullet)
        {
            if (other.CompareTag("Enemy"))
            {
                // EnemyManager.Instance.DestroyEnemyInSameLine(other.gameObject);
                
                BulletHitEnemy(other.gameObject);
                DestroyBullet();
            }
            
            if (other.CompareTag("Bullet"))
                DestroyBullet();
            
            if (other.CompareTag("Shield"))
                BulletHitShield(other.gameObject);
        }
        else
        {
            if (other.CompareTag("Player")) 
                BulletHitPlayer(other.gameObject);
            if (other.CompareTag("Bullet"))
            {
                SoundManager.Instance.PlaySound("ProjectileDestroyed");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            if (IsEnemyBullet)
            {
                if (CanBounce)
                {
                    Direction *= -1;
                    CanBounce = false;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                DestroyBullet();
            }
        }
    }
}
