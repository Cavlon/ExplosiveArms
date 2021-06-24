using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{

    public List<LevelInfo> levels = new List<LevelInfo>();
    private int levelNo;

    void Start()
    {
        levelNo = 0;
        NewLevel();
    }


    public void NewLevel()
    {
        if (levelNo < 9)
        {
            NormalLevel();
            levelNo += 1;
        }
        else
        {
            print("Boss Level Here");
        }  
        Invoke("ReScan", 1f);
    }

    private void ReScan()
    {
        AstarPath.active.Scan();
    }

    private void NormalLevel()
    {
        int randVal = Random.Range(0, levels.Count);
        Instantiate(levels[randVal]);
        levels.RemoveAt(randVal);
    }
}
