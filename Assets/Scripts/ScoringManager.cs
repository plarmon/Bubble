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

    private int minutes;
    private float seconds;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AudioSource ding;
    
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
        ding = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_levelInProgress)
            _levelTime += Time.deltaTime;
        minutes = (int)_levelTime/60;
        seconds = _levelTime % 60f;
        _timeText.text = minutes + ":" + seconds.ToString("00.0");
    }

    public void LevelComplete()
    {
        _levelInProgress = false;
    }

    public void AddToScore(int points) {
        _score += points;
        ding.Play();
        if(_scoreText)
        {
            _scoreText.text = _score.ToString();
        }
    }

    public int GetScore() {
        return _score;
    }

    public string GetTime() {
        return minutes + ":" + seconds.ToString("00.0");
    }
}
