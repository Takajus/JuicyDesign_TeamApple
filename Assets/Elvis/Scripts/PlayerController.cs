using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    private float boostSpeed;
    private float _speedTemp;
    
    [Header("Weapon")]
    [SerializeField]
    private GameObject weaponPosition;
    [SerializeField]
    private GameObject bullet;
    [SerializeField] 
    private GameObject bfg;
    [SerializeField] 
    private GameObject body;

    private bool _canShot = true;
    private bool _canUseBfg = false;
    
    private bool _useBoost = false;
    private int _currentDir = 0;
    private int _indexHit = 0;
    
    private bool _canMoveLeft = true;
    private bool _canMoveRight = true;
    
    [SerializeField]
    private VolumeProfile _volumeProfile;

    [SerializeField]
    private GameObject[] effectHit;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    { 
        _volumeProfile.TryGet(out Vignette vignette);
        vignette.intensity.value = 0;
        
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
        float horizontal = JuicyManager.Instance.vectorTest();
        Vector3 direction = new Vector3(horizontal, 0, 0);
        
        if (horizontal > 0 && _currentDir != 1)
        {
            _currentDir = 1;
            
            JuicyManager.Instance.Propulsion(-1);
            
            if (!_useBoost)
                StartCoroutine(LerpBoost());
        }
        else if (horizontal < 0 && _currentDir != -1)
        {
            _currentDir = -1;
            
            JuicyManager.Instance.Propulsion(1);
            
            if (!_useBoost)
                StartCoroutine(LerpBoost());
        }
        
        if (_canMoveLeft && horizontal <= 0 || _canMoveRight && horizontal >= 0)
            transform.position += direction * (speed * Time.deltaTime);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shot()
    {
        if (JuicyManager.Instance.Fire1())
        {
            Shoting(bullet, "Shot");
            
            JuicyManager.Instance.StartShooting();
        }
        else if (JuicyManager.Instance.Fire2())
        {
            if (_canUseBfg)
            {
                Shoting(bfg, "BFGShot");
                GameManager.Instance.ResetBFG();
                _canUseBfg = false;
            }
            else
            {
                JuicyManager.Instance.PopUpScoreSystem(gameObject, "Can not use BFG");
                SoundManager.Instance.PlaySound("BFGUnavailable");
            }
        }
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
        GetComponent<CameraShake>().enabled = true;

        health -= damage;
        
        if (health <= 0)
        {
            KillPlayer();
        }
        else
        {
            SetIntensity(0.34f);
            
            HitEffect();
            
            SoundManager.Instance.PlaySound("BFGHit");
        }
    }

    private void KillPlayer()
    {
        DeathEffect();
        body.SetActive(false);
        
        GameManager.Instance.SetEndGame();
    }

    private void DeathEffect()
    {
        JuicyManager.Instance.DestructionSystem(body);
    }

    public void HitEffect()
    {
        _indexHit++;

        if (_indexHit < effectHit.Length)
        {
            GameObject hitEffect = Instantiate(effectHit[_indexHit], transform.position, Quaternion.identity);
            hitEffect.transform.SetParent(gameObject.transform);
        }
    }
    
    public void SetIntensity(float intensity)
    {
        _volumeProfile.TryGet(out Vignette vignette);
        vignette.intensity.value += intensity;
    }

    private IEnumerator LerpBoost()
    {
        _useBoost = true;
        
        float t = 0;
        float startSpeed = boostSpeed;
        float endSpeed = speed;
        
        while (t < 1)
        {
            t += Time.deltaTime;
            speed = Mathf.Lerp(startSpeed, endSpeed, t);
            yield return null;
        }

        speed = _speedTemp;
        _useBoost = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left"))
            _canMoveLeft = false;
        
        if (other.CompareTag("Right"))
            _canMoveRight = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left"))
            _canMoveLeft = true;
        
        if (other.CompareTag("Right"))
            _canMoveRight = true;
    }
}
