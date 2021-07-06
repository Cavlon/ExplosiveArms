using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [HideInInspector] public int highscore;
    [HideInInspector] public bool beatenGame;
    [HideInInspector] public int musicVal;
    [HideInInspector] public int sfxVal;

    public bool loaded;

    private void Start()
    {
        loaded = false;
        Load();
    }

    public void Save()
    {
        SaveGame.SaveData(this);
    }

    public void Load()
    {
        SaveData data = SaveGame.LoadData();
        highscore = data.highscore;
        beatenGame = data.beatenGame;
        musicVal = data.musicVal;
        sfxVal = data.sfxVal;
        loaded = true;
    }
}
