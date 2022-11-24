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
    private int health;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float _boostSpeed;

    private float _speedTemp;
    
    [Header("Weapon")]
    [SerializeField]
    private GameObject weaponPosition;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField] 
    private GameObject bfg;


    [Header("Sound")]
    [SerializeField] 
    private string soundBullet;
    [SerializeField] 
    private string _bfgSound;

    private bool _canShot = true;
    private bool _canUseBfg = false;
    
    private bool _useBoost = false;
    private int _currentDir = 0;

    // [SerializeField]
    // private AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        _speedTemp = _speed;
    }

    private void Start()
    {
        _speedTemp = speed;
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
        //float horizontal = Input.GetAxis("Horizontal");
        //Vector3 direction = new Vector3(horizontal, 0, 0);
        float horizontal = JuicyManager.Instance.vectorTest();
        Vector3 direction = new Vector3(horizontal, 0, 0);
        
        if (horizontal > 0 && _currentDir != 1)
        {
            _currentDir = 1;
            
            if (!_useBoost)
                StartCoroutine(LerpBoost());
        }
        else if (horizontal < 0 && _currentDir != -1)
        {
            _currentDir = -1;
            
            if (!_useBoost)
                StartCoroutine(LerpBoost());
        }
        
        transform.position += direction * (_speed * Time.deltaTime);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shot()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            Shoting(_bullet, "Shot");
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (_canUseBfg)
            {
                Shoting(_bullet, "BFGShot");
                GameManager.Instance.ResetBFG();
                _canUseBfg = false;
                GameManager.Instance.ResetRageBFG();
            }
            else
            {
                JuicyManager.Instance.PopUpScoreSystem(gameObject, "Can not use BFG");
                SoundManager.Instance.PlaySound("BFGUnavailable");
            }
        }*/
        
        if (JuicyManager.Instance.Fire1())
        {
            Shoting(_bullet, "Shot");
        }
        else if (JuicyManager.Instance.Fire2())
        {
            if (_canUseBfg)
            {
                Shoting(_bullet, "BFGShot");
                GameManager.Instance.ResetBFG();
                _canUseBfg = false;
            }
            else
            {
                JuicyManager.Instance.PopUpScoreSystem(gameObject, "Can not use BFG");
                SoundManager.Instance.PlaySound("BFGUnavailable");
            }
        }

        // StartCoroutine(ShotTimer());
    }

    private void Shoting(GameObject obj, string sound)
    {
        if (obj)
            Instantiate(obj, weaponPosition.transform.position, Quaternion.Euler(90, 0, 0));
        
        SoundManager.Instance.PlaySound(sound);
        _canShot = false;
    }

    public void SetCanShot(bool value = true)
    {
        _canShot = true;
    }
    
    public void SetCanUseBfg(bool value = true)
    {
        _canUseBfg = value;
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
            SoundManager.Instance.PlaySound("PlayerHit");
    }

    private void KillPlayer()
    {
        SoundManager.Instance.PlaySound("PlayerDeath");
        //gameObject
        GameManager.Instance.SetEndGame();
    }

    private IEnumerator LerpBoost()
    {
        _useBoost = true;
        
        float t = 0;
        float startSpeed = _boostSpeed;
        float endSpeed = _speed;
        
        while (t < 1)
        {
            t += Time.deltaTime;
            _speed = Mathf.Lerp(startSpeed, endSpeed, t);
            yield return null;
        }

        _speed = _speedTemp;
        _useBoost = false;
    }
}
