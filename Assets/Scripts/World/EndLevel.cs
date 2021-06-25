using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{

    private LevelInfo level;

    private void Awake()
    {
        level = transform.parent.GetComponent<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            level.endLevel = true;
        }
    }
}
