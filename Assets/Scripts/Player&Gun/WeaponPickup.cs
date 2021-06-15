using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    [SerializeField] protected Transform weapon;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" & player.GetComponent<Player_Shooting>().hasGun == false)
        {
            Instantiate(weapon, player.transform.position, player.transform.rotation, player.transform);
            player.GetComponent<Player_Shooting>().GetGun();
            gameManager.GetComponent<DetectControlMethod>().GetWeapon();
            gameManager.GetComponent<EnemyDeath>().droppedGuns -= 1;
            Destroy(gameObject);
        }
    }
}
