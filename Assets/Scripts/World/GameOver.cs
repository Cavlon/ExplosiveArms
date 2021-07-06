using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] protected Canvas deadCanvas;
    [SerializeField] protected Canvas winCanvas;
    [SerializeField] protected Canvas mainCanvas;
    [SerializeField] protected Image deathEffect;
    private Transform player;
    private Camera cam;
    private SpawnLevel spawnLevel;
    private DetectControlMethod controls;
    private EnemyDeath enemyDeath;
    private PlayerMovement playerMove;

    void Start()
    {
        spawnLevel = GetComponent<SpawnLevel>();
        controls = GetComponent<DetectControlMethod>();
        enemyDeath = GetComponent<EnemyDeath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMovement>();
cam = Camera.main;
    }

    public void EndGame(bool dead)
    {
        controls.gameOver = true;
        enemyDeath.gameOver = true;
        playerMove.gameOver = true;
        mainCanvas.enabled = false;  
        if (dead)
        {
            Canvas endScreen = Instantiate(deadCanvas);
            endScreen.GetComponentInChildren<Animator>().SetTrigger("Gameover");
            spawnLevel.DeleteLevel();
            StartCoroutine(Explosion(endScreen));
        } else
        {
            Instantiate(winCanvas);
        }
    }

    IEnumerator Explosion(Canvas endScreen){
        yield return new WaitForSeconds(1f);
        Vector2 effectPos = cam.WorldToScreenPoint(player.position);
        RectTransform rt = endScreen.GetComponent<RectTransform>();
        Image Explosion = Instantiate(deathEffect, effectPos, Quaternion.identity, endScreen.transform);
        Explosion.rectTransform.sizeDelta = rt.sizeDelta;
    }
}
