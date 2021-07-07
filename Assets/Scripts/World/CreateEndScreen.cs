using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateEndScreen : MonoBehaviour
{

    [SerializeField] protected TextMeshProUGUI highscore;
    [SerializeField] protected TextMeshProUGUI currentScore;
    [SerializeField] protected TextMeshProUGUI endTitle;
    [SerializeField] protected Button retryButton;
    [SerializeField] protected Button exitButton;
    [SerializeField] protected bool dead;

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
        if (!dead && enemyDeath.totalScore > info.highscore)
        {
            highscore.text = "Highscore:" + enemyDeath.totalScore;
            info.highscore = enemyDeath.totalScore;
            info.beatenGame = true;
            info.Save();
        } else
        {
            highscore.text = "Highscore:" + info.highscore;
        }       
        currentScore.gameObject.SetActive(true);
        currentScore.text = "Score:" + enemyDeath.totalScore;
        endTitle.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
}
