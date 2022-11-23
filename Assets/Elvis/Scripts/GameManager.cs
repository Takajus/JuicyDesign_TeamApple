using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private SoundManager _soundManager;
    private bool _isGameIsEnd = false;

    [SerializeField]
    private int _rageMaxValue = 200;

    [Range(0, 200)]
    [SerializeField]
    private int _bfgRage;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        _soundManager = FindObjectOfType<SoundManager>();_soundManager = FindObjectOfType<SoundManager>();

    }

    private void Start()
    {
        // _soundManager.PlaySound("Theme");
    }

    public void SetRageBFG(int value = 0)
    {
        if (_bfgRage >= _rageMaxValue)
        {
            PlayerController.Instance.SetCanUseBfg();
        }
        else
        {
            _bfgRage += value;
        }
    }

    public void ResetRageBFG()
    {
        _bfgRage = 0;
    }
    
    private void EndGame()
    {
        StopAllCoroutines();
        // Time.timeScale = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void SetEndGame()
    {
        _isGameIsEnd = true;
        EndGame();
    }

    public bool GetIsEndGame()
    {
        return _isGameIsEnd;
    }
}
