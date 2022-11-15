using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate;
    
    private bool _canShot = true;

    [SerializeField]
    private float _raycastDistance;
    
    private PlayerController _player;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!bullet)
            Resources.Load($"Bullet");

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, 
                -transform.up, out var hit, _raycastDistance))
        {
            if (!hit.collider.CompareTag("Enemy"))
            {
                if (Math.Abs(_player.GetPlayerXPos() - transform.position.x) < .75f)
                    Shot();
            }
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void Shot()
    {
        if (_canShot)
        {
            _canShot = false;
            StartCoroutine(ShotDelay());
            GameObject bulletInstance = Instantiate(bullet, weapon.transform.position, Quaternion.identity);
            
            bulletInstance.GetComponent<BulletController>().SetDirection(-1);
        }
    }
    
    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(fireRate);
        _canShot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * _raycastDistance);
    }
}