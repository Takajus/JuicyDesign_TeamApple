using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private SoundManager _soundManager;
    private bool _isGameIsEnd = false;

    public GameObject gameoverPanel;
    public TextMeshProUGUI scoreValue;

    [Range(0, 200)]
    [SerializeField]
    private int _bfgRage;

    [SerializeField]
    private int maxRage;

    private int score;
    
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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Menu")
        {
            _soundManager.PlaySound("MenuMusic");
        }
        else if(scene.name == "SampleScene")
        _soundManager.PlaySound("GameMusic");
    }

    public void SetRageBFG(int rage)
    {
        if (_bfgRage >= maxRage)
        {
            PlayerController.Instance.SetCanUseBFG();
            SoundManager.Instance.PlaySound("BFGAvailable");
        }
        else
        {
            _bfgRage += rage;
        }
    }

    public void ResetBFG()
    {
        _bfgRage = 0;
    }
    
    private void EndGame()
    {
        StopAllCoroutines();
        // Time.timeScale = 0;
        SoundManager.Instance.PlaySound("Gameover");
        gameoverPanel.SetActive(true);
        scoreValue.text = score.ToString();
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void replayButton()
    {
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
