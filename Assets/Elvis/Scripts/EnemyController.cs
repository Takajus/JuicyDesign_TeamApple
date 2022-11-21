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
    private float raycastDistance;
    [SerializeField]
    private LayerMask layerMask;
    
    private PlayerController _player;
    
    // Start is called before the first frame update
    public void Start()
    {
        if (!bullet)
            Resources.Load($"Bullet");

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!Physics.Raycast(transform.position, 
                -transform.up, out var hit, raycastDistance, layerMask))
        {
            if (Math.Abs(_player.GetPlayerXPos() - transform.position.x) < .75f)
                Shot();
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void Shot()
    {
        if (EnemyManager.Instance.GetCanShot())
        {
            EnemyManager.Instance.SetCanShotTrue(false);
            GameObject bulletInstance = Instantiate(bullet, weapon.transform.position, Quaternion.Euler(90, 0, 0));
            
            bulletInstance.GetComponent<BulletController>().SetDirection(-1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * raycastDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
            GameManager.Instance.SetEndGame();
    }
}
