using System;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{

    private bool startTimer = false;
    private float time;
    [SerializeField] TextMeshProUGUI timerText;
    
    void Start()
    {
        time = 0;
        StartTimer();
    }

    
    void Update()
    {
        if (startTimer == true)
        {
            time += Time.deltaTime;
        }
        TimeSpan timeFormat = TimeSpan.FromSeconds(time);
        timerText.text = timeFormat.ToString(@"mm\:ss\:fff");
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
    }
}
