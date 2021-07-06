using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateOptions : MonoBehaviour
{

    [SerializeField] protected TextMeshProUGUI highscore;
    [SerializeField] protected TextMeshProUGUI currentScore;
    [SerializeField] protected Button retryButton;
    [SerializeField] protected Button exitButton;

    private GameInfo info;
    private EnemyDeath enemyDeath;

    void Awake()
    {
        info = FindObjectOfType<GameInfo>();
        enemyDeath = info.GetComponent<EnemyDeath>();
    }

    public void SpawnOptions()
    {
        Cursor.visible = true;
        highscore.gameObject.SetActive(true);
        highscore.text = "Highscore:" + info.highscore;
        currentScore.gameObject.SetActive(true);
        currentScore.text = "Score:" + enemyDeath.totalScore;
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
}
