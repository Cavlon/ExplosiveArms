using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrops : MonoBehaviour
{
    public int droppedGuns;
    public List<GameObject> availableWeapons = new List<GameObject>();
    private int probabilityWindow;

    void Start()
    {
        droppedGuns = 0;
        probabilityWindow = 70;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Drop(Transform enemyPos)
    {

        int randomChance = Random.Range(0, 101);
        if (randomChance < probabilityWindow)
        {
            Instantiate(availableWeapons[Random.Range(0, availableWeapons.Count)], enemyPos.position, Quaternion.identity);
            droppedGuns += 1;
        }
    }

    private void probability()
    {
        probabilityWindow = Mathf.RoundToInt((float)(100 * (0.6 / (droppedGuns + 1))));
        print(probabilityWindow);
    }

    public void deadEnemy(Transform enemyPos)
    {
        probability();
        Drop(enemyPos);
    }
}
