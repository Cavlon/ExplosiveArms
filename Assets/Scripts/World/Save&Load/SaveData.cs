[System.Serializable]
public class SaveData
{
    public int highscore;
    public bool beatenGame;
    public int musicVal;
    public int sfxVal;

    public SaveData(GameInfo info)
    {
        highscore = info.highscore;
        beatenGame = info.beatenGame;
        musicVal = info.musicVal;
        sfxVal = info.sfxVal;
    }
}
