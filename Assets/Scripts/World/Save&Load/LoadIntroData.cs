using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadIntroData : MonoBehaviour
{

    public Slider musicSlider;
    public Slider sfxSlider;
    [SerializeField] TextMeshProUGUI highscoreText;

    private GameInfo data;

    private void Start()
    {
        data = GetComponent<GameInfo>();
    }

    void Update()
    {
        if (data.loaded)
        {
            musicSlider.value = data.musicVal;
            sfxSlider.value = data.sfxVal;
            if (data.beatenGame)
            {
                highscoreText.enabled = true;
                highscoreText.text = "Highscore:" + data.highscore;
            }
            Destroy(this);
        }
    }
}
