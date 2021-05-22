using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    private int health;
    private int heartNum;
    public PlayerController playerCont;

    [SerializeField] protected Image[] hearts;
    [SerializeField] protected Sprite heart;
    [SerializeField] protected Sprite emptyHeart;

    private void Start()
    {
    }

    void Update()
    {
    }

    public void UpdateHealth()
    {
        health = playerCont.health;
        heartNum = playerCont.maxHealth;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < heartNum)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
