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
    
    private GameObject _weapon;
    
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
            Instantiate(bullet, weaponPosition.transform.position, Quaternion.Euler(90, 0, 0));
        
        _canShot = false;

        StartCoroutine(ShotTimer());
    }
    
    private void SwapWeapon()
    {
        _weapon.gameObject.SetActive(false);
        
        int currIdx = weapons.IndexOf(_weapon);
        int nextIdx = (currIdx + 1) % weapons.Count;
        
        _weapon = weapons[nextIdx];
        _weapon.gameObject.SetActive(true);
    }
    
    public int GetPlayerXPos()
    {
        return (int) transform.position.x;
    }
    
    public int GetPlayerYPos()
    {
        return (int) transform.position.y;
    }

    public int GetHealth()
    {
        return health;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            KillPlayer();
        }
        else
            _soundManager.PlaySound("PlayerHit");
    }

    private void KillPlayer()
    {
        _soundManager.PlaySound("PlayerDeath");
        GameManager.Instance.SetEndGame();
    }
    
    private IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(fireRate);
        _canShot = true;
    }
}
