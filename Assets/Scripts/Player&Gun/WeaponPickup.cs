using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    private Player_Shooting playerShooting;
    [SerializeField] protected Transform weapon;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponent<Player_Shooting>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" & player.GetComponent<Player_Shooting>().hasGun == false)
        {
            playerShooting.DestroyMelee();
            Instantiate(weapon, player.transform.position, player.transform.rotation, player.transform);            
            gameManager.GetComponent<DetectControlMethod>().GetWeapon();
            gameManager.GetComponent<EnemyDeath>().droppedGuns -= 1;
            Destroy(gameObject);
        }
    }
}
