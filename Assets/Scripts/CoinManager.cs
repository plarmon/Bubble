using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private static CoinManager _instance;
    public static CoinManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void AddToScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }
}
