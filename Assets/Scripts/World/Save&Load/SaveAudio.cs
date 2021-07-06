using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAudio : MonoBehaviour
{

    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider sfxSlider;
    private GameInfo data;
    private LoadIntroData loadData;

    void Start()
    {
        data = transform.parent.parent.GetComponent<GameInfo>();
        loadData = data.GetComponent<LoadIntroData>();
    }

    public void SaveSliderData()
    {
        data.musicVal = (int)musicSlider.value;
        data.sfxVal = (int)sfxSlider.value;
        data.Save();
    }
}
