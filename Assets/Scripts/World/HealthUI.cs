using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    private int health;
    private int heartNum;   
    [HideInInspector] public PlayerController playerCont;

    [SerializeField] protected Animator[] heartsAnim;
    private List<Image> heartsImage = new List<Image>();

    public void UpdateHealth()
    {
        health = playerCont.health;
        heartNum = playerCont.maxHealth;
        for (int i = 0; i < heartsAnim.Length; i++)
        {
            if (i < health)
            {
                heartsAnim[i].SetBool("Full", true);
            }
            else
            {
                heartsAnim[i].SetBool("Full", false);
            }

            if (i < heartNum)
            {
                heartsImage[i].enabled = true;
            }
            else
            {
                heartsImage[i].enabled = false;
            }
        }
    }

    public void getImages()
    {
        for (int i = 0; i < heartsAnim.Length; i++)
        {
            heartsImage.Add(heartsAnim[i].gameObject.GetComponent<Image>());
        }
        heartsImage.ToArray();
    }
}
