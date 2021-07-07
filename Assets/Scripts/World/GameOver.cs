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
        PauseGame.gameOver = true;
        if (dead)
        {
            Canvas endScreen = Instantiate(deadCanvas);
            endScreen.GetComponentInChildren<Animator>().SetTrigger("Gameover");
            spawnLevel.DeleteLevel();
            StartCoroutine(Explosion(endScreen, cam.WorldToScreenPoint(player.position), true, 1.9f, 1f));
        } else
        {
            Canvas endScreen = Instantiate(winCanvas);
            endScreen.GetComponentInChildren<Animator>().SetTrigger("Gameover");
            spawnLevel.DeleteLevel();
            RectTransform rt = endScreen.GetComponent<RectTransform>();
            Vector2 pos = new Vector2(Screen.width/2, -(Screen.height / 2) + (rt.sizeDelta.y /2));
            StartCoroutine(Explosion(endScreen, pos, false, 0.9f, 2.5f));
        }
    }

    IEnumerator Explosion(Canvas endScreen, Vector2 pos, bool dead, float destroytime, float sizeMultiplier){
        yield return new WaitForSeconds(1f);
        Vector2 effectPos = pos;
        RectTransform rt = endScreen.GetComponent<RectTransform>();
        Image Explosion = Instantiate(deathEffect, effectPos, Quaternion.identity, endScreen.transform);
        Animator explosionAnim = Explosion.GetComponent<Animator>();
        explosionAnim.SetBool("Dead", dead);
        Explosion.GetComponent<EffectDestroy>().destroyTime = destroytime;
        Explosion.rectTransform.sizeDelta = rt.sizeDelta * sizeMultiplier;
    }
}
