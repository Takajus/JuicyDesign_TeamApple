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

    private bool _isEnemyBullet = false;
    private bool _canBounce = false;
    private int _direction = 1;
    
    protected void Start()
    {
        Destroy(gameObject, lifeTime);

        _visualEffect = GetComponent<VisualEffect>();

        _visualEffect.SetFloat("lifetime", lifeTime);
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += Vector3.forward * (_direction * (speed * Time.deltaTime));
    }

    public void SetIsEnemyBullet(bool value = false)
    {
        if (!value)
        {
            SetDirection(1);
            _isEnemyBullet = false;
        }
        else
        {
            SetDirection(-1);
            _isEnemyBullet = true;
            _canBounce = true;
        }
        
    }
    
    private void SetDirection(int direction)
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
        int point = UnityEngine.Random.Range(10, 20);
        
        JuicyManager.Instance.DestructionSystem(enemy.gameObject);
        JuicyManager.Instance.PopUpScoreSystem(enemy.gameObject, $"{point}");
                
        SoundManager.Instance.PlaySound("Destruction alien");
        
        GameManager.Instance.SetRageBFG(point);
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
        if (!_isEnemyBullet)
        {
            if (other.CompareTag("Enemy"))
            {
                // EnemyManager.Instance.DestroyEnemyInSameLine(other.gameObject);
                
                BulletHitEnemy(other.gameObject);
                DestroyBullet();
            }
            
            if (other.CompareTag("Bullet"))
                DestroyBullet();
        }
        else
        {
            if (other.CompareTag("Player")) 
                BulletHitPlayer(other.gameObject);
            if (other.CompareTag("Bullet"))
                Destroy(gameObject);
        }
        
        if (other.CompareTag("Shield"))
            BulletHitShield(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            if (_isEnemyBullet)
            {
                if (_canBounce)
                {
                    _direction *= -1;
                    _canBounce = false;
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
