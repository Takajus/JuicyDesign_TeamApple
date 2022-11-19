using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 2f;

    protected int _direction = 1;
    
    protected SoundManager SoundManager;

    protected void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
        // _soundManager.PlaySound("PlayerShot");

        Destroy(gameObject, lifeTime);
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
        player.GetComponent<PlayerController>().GetDamage(1);
        Destroy(gameObject);
    }
    
    private void BulletHitEnemy(GameObject enemy)
    {
        JuicyManager.Instance.DestructionSystem(enemy.gameObject);
        JuicyManager.Instance.PopUpScoreSystem(enemy.gameObject, "13");
                
        SoundManager.PlaySound("Destruction alien");
                
        Destroy(enemy.gameObject);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_direction == 1)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyManager.Instance.DestroyEnemyInSameLine(other.gameObject);
                // BulletHitEnemy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player")) 
            {
                BulletHitPlayer(other.gameObject);
            }
        }
    }
}
