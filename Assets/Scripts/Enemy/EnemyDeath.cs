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

    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected TextMeshProUGUI comboText;
    [SerializeField] protected Animator scoreAnim;
    [SerializeField] protected Animator comboAnim;

    void Start()
    {
        droppedGuns = 0;
        combo = 1;
        Combo(false);
        AddScore(0);
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
        Combo(true);
    }

    private void AddScore(int score)
    {
        totalScore += score * combo;
        scoreText.text = "Score:" + totalScore;
        scoreAnim.SetTrigger("Score");
    }

    private void Combo(bool addVal)
    {
        if (addVal)
        {
            combo += 1;
            comboText.text = "Combo x" + combo;
            comboAnim.SetTrigger("Combo");
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
}
