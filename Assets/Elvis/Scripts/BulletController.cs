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
    
    private int _direction = 1;
    
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, lifeTime);

        visualEffect = GetComponent<VisualEffect>();

        visualEffect.SetFloat("lifetime", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.forward * (_direction * (speed * Time.deltaTime));
    }
    
    public void SetDirection(int direction)
    {
        _direction = direction;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_direction == 1)
        {
            if (other.CompareTag("Enemy"))
            {
                JuicyManager.Instance.DestructionSystem(other.gameObject);
                JuicyManager.Instance.PopUpScoreSystem(other.gameObject, "13");
                
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player")) 
            {
                other.GetComponent<PlayerController>().GetDamage(1);
                Destroy(gameObject);
            }
        }
    }
}
