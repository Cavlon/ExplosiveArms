using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{

    public List<LevelInfo> levels = new List<LevelInfo>();

    void Start()
    {        
        NewLevel();
    }


    public void NewLevel()
    {
        int randVal = Random.Range(0, levels.Count);
        Instantiate(levels[randVal]);
        levels.RemoveAt(randVal);
        Invoke("ReScan", 1f);
    }

    private void ReScan()
    {
        AstarPath.active.Scan();
    }
}
