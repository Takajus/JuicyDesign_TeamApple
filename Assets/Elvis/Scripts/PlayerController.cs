using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get { return _instance;  }
    }
    
    [Header("Stats")]
    [SerializeField] 
    private int _health;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fireRate;
    
    [Header("Weapon")]
    [SerializeField]
    private GameObject _weaponPosition;
    [SerializeField]
    private GameObject _bullet;

    private GameObject _weapon;
    
    [SerializeField]
    private List<GameObject> _weapons;

    private bool _canShot = true;
    
    // [SerializeField]
    // private AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_canShot)
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
        
        transform.position += direction * (_speed * Time.deltaTime);
        // transform.Translate(direction * (speed * Time.deltaTime));
    }
    
    private void Shot()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        
        if (_bullet)
            Instantiate(_bullet, _weaponPosition.transform.position, Quaternion.Euler(90, 0, 0));
        
        SoundManager.Instance.PlaySound("PlayerShot");
        _canShot = false;

        // StartCoroutine(ShotTimer());
    }

    public void SetCanShot(bool value = true)
    {
        _canShot = true;
    }
    
    private void SwapWeapon()
    {
        _weapon.gameObject.SetActive(false);
        
        int currIdx = _weapons.IndexOf(_weapon);
        int nextIdx = (currIdx + 1) % _weapons.Count;
        
        _weapon = _weapons[nextIdx];
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
        return _health;
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            KillPlayer();
        }
        else
            SoundManager.Instance.PlaySound("PlayerHit");
    }

    private void KillPlayer()
    {
        SoundManager.Instance.PlaySound("PlayerDeath");
        GameManager.Instance.SetEndGame();
    }
    
    private IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShot = true;
    }
}
