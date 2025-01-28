using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private CanvasGroup finalPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;

    private void Start() {
        finalPanel.alpha = 0;
    }

    public void EndGame() {
        finalScoreText.text = ScoringManager.Instance.GetScore().ToString();
        finalTimeText.text = ScoringManager.Instance.GetTime(); 
        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine() {
        float alpha = 0;
        while(alpha < 1) {
            alpha += Time.deltaTime;
            if(alpha > 1) {
                alpha = 1;
            }
            finalPanel.alpha = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("MainMenu");
    }
}
