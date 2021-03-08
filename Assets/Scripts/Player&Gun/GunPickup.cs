using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    [SerializeField] protected GameObject gun;

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
            Instantiate(gun, player.transform.position, player.transform.rotation, player.transform);
            player.GetComponent<Player_Shooting>().GetGun();
            gameManager.GetComponent<DetectControlMethod>().GetGun();
            gameManager.GetComponent<WeaponDrops>().droppedGuns -= 1;
            Destroy(gameObject);
        }
    }
}
