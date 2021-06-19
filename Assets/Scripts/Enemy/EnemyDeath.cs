using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDeath : MonoBehaviour
{ 
    
    [HideInInspector] public int droppedGuns;
     public int droppedHealth;

    private int probabilityWindow;
    private int totalScore;
    private int combo;
    private bool firstKill;
    private float lastKillTime;
    private EnemySpawning spawning;
    private PlayerController player;

    [SerializeField] protected List<weaponScript> availableWeapons;
    [SerializeField] protected WeaponPickup weaponPickup;
    [SerializeField] protected Transform healthPickup;
    [SerializeField] protected Transform healthUpgrade;    
    [SerializeField] protected float comboResetTime;
    [SerializeField] protected float weaponDropChance;
    [SerializeField] protected float healthDropChance;

    [Header("UI")]
    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected TextMeshProUGUI comboText;
    [SerializeField] protected Animator scoreAnim;
    [SerializeField] protected Animator comboAnim;

    private void Start()
    {
        spawning = GetComponent<EnemySpawning>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        weaponDropProb();
        if (randomChance < probabilityWindow)
        {
            WeaponPickup instance = Instantiate(weaponPickup, enemyPos.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            int randomVal = Random.Range(0, availableWeapons.Count);
            weaponScript chosenWeapon = availableWeapons[randomVal];
            instance.weapon = chosenWeapon;
            instance.GetComponent<SpriteRenderer>().sprite = chosenWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
            droppedGuns += 1;
        }

        randomChance = Random.Range(0, 101);
        if (randomChance < healthDropChance * 10)
        {
            Instantiate(healthUpgrade, enemyPos.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))));
        } else if (randomChance < healthDropChance * 100 && player.health + droppedHealth < player.maxHealth)
        {
            Instantiate(healthPickup, enemyPos.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))));
            droppedHealth += 1;
        }
    }

    private void weaponDropProb()
    {
        probabilityWindow = Mathf.RoundToInt((float)(100 * (weaponDropChance / (droppedGuns + 1))));
    }

    public void deadEnemy(Transform enemyPos, int score)
    {
        spawning.currentEnemies -= 1;
        Drop(enemyPos);
        AddScore(score);
        Combo();
    }

    private void AddScore(int score)
    {
        totalScore += score * combo;
        scoreText.text = "Score:" + totalScore;
        if (scoreAnim != null)
        {
            scoreAnim.SetTrigger("Score");
        }        
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
