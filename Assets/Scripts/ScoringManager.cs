using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;

public class ScoringManager : MonoBehaviour
{
    private float _levelTime;
    private bool _levelInProgress;
    private int _score = 0;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public static ScoringManager Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    void Start()
    {
        _score = 0;
        _levelTime = 0f;
        _levelInProgress = true;
    }

    void Update()
    {
        if(_levelInProgress)
            _levelTime += Time.deltaTime;
        int minutes = (int)_levelTime/60;
        float seconds = _levelTime % 60f;
        _timeText.text = minutes + ":" + seconds.ToString("00.0");
    }

    public void LevelComplete()
    {
        _levelInProgress = false;
    }

    public void AddToScore(int points) {
        _score += points;
        if(_scoreText)
        {
            _scoreText.text = _score.ToString();
        }
    }
}
