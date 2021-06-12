using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDeath : MonoBehaviour
{
    public int droppedGuns;
    public List<GameObject> availableWeapons = new List<GameObject>();

    private int probabilityWindow;
    private int totalScore;
    private int combo;
    private bool firstKill;
    private float lastKillTime;

    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected TextMeshProUGUI comboText;
    [SerializeField] protected Animator scoreAnim;
    [SerializeField] protected Animator comboAnim;
    [SerializeField] protected float comboResetTime;

    private void Start()
    {
        droppedGuns = 0;
        combo = 1;
        firstKill = true;
        lastKillTime = 0;
        AddScore(0);
    }

    private void Update()
    {
        if (Time.time - lastKillTime > comboResetTime)
        {
            combo = 1;
            firstKill = true;
        }
        if (combo == 1)
        {
            comboText.enabled = false;
        }
        else
        {
            comboText.enabled = true;
        }
    }

    private void Drop(Transform enemyPos)
    {

        int randomChance = Random.Range(0, 101);
        if (randomChance < probabilityWindow)
        {
            Instantiate(availableWeapons[Random.Range(0, availableWeapons.Count)], enemyPos.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))));
            droppedGuns += 1;
        }
    }

    private void probability()
    {
        probabilityWindow = Mathf.RoundToInt((float)(100 * (0.6 / (droppedGuns + 1))));
    }

    public void deadEnemy(Transform enemyPos, int score)
    {
        probability();
        Drop(enemyPos);
        AddScore(score);
        Combo();
    }

    private void AddScore(int score)
    {
        totalScore += score * combo;
        scoreText.text = "Score:" + totalScore;
        scoreAnim.SetTrigger("Score");
    }

    private void Combo()
    {
        
            
        if (firstKill)
        {
            firstKill = false;
        }
        else
        {
            combo += 1;
        }
        comboText.text = "Combo x" + combo;
        comboAnim.SetTrigger("Combo");            
        lastKillTime = Time.time;
                  
    }
}
