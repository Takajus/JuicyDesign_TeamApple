using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private SoundManager _soundManager;
    private bool _isGameIsEnd = false;

    public GameObject gameoverPanel, VrGameoverpanel;
    public TextMeshProUGUI scoreValue, VrScoreValue;
    
    private int _bfgRage;
    [SerializeField]
    public Slider bfgSlider;

    [SerializeField]
    private int maxRage;

    private int _score;
    
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


        bfgSlider.minValue = 0;
        bfgSlider.maxValue = maxRage;
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

    private void Update()
    {
        bfgSlider.value = _bfgRage;
    }

    public void SetRageBFG(int rage)
    {
        if (_bfgRage >= maxRage)
        {
            PlayerController.Instance.SetCanUseBfg();
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
        if (JuicyManager.Instance.VrVsInput)
        {
            VrGameoverpanel.SetActive(true);
            VrScoreValue.text = _score.ToString();
        }
        else if (!JuicyManager.Instance.VrVsInput)
        {
            gameoverPanel.SetActive(true);
            scoreValue.text = _score.ToString();
        }
    }

    public void IncreaseScore(int value)
    {
        _score += value;
    }

    public void ReplayButton()
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
