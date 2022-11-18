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
        _soundManager.PlaySound("Theme");
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
