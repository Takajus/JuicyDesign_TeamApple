using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] 
    private int health;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float fireRate;
    
    [Header("Weapon")]
    [SerializeField]
    private GameObject weaponPosition;
    [SerializeField]
    private GameObject bullet;
    
    private GameObject weapon;
    
    [SerializeField]
    private List<GameObject> weapons;
    
    private bool _canShot = true;
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        Shot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 0);
        
        transform.position += direction * (speed * Time.deltaTime);
        // transform.Translate(direction * (speed * Time.deltaTime));
    }
    
    private void Shot()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        if (!_canShot) return;

        if (bullet)
            Instantiate(bullet, weaponPosition.transform.position, Quaternion.identity);
        
        _canShot = false;

        StartCoroutine(ShotTimer());
    }
    
    private void SwapWeapon()
    {
        weapon.gameObject.SetActive(false);
        
        int currIdx = weapons.IndexOf(weapon);
        int nextIdx = (currIdx + 1) % weapons.Count;
        
        weapon = weapons[nextIdx];
        weapon.gameObject.SetActive(true);
    }
    
    public int GetPlayerXPos()
    {
        return (int) transform.position.x;
    }

    public int GetHealth()
    {
        return health;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
            KillPlayer();
        else
            _soundManager.PlaySound("PlayerHit");
    }

    private void KillPlayer()
    {
        _soundManager.PlaySound("PlayerDeath");
    }
    
    private IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(fireRate);
        _canShot = true;
    }
}
